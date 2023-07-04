using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Services
{
    public class ThreadService : IThreadService
    {
        private const string UnauthorizedErrorMessage = "Only author or admin can modify a comment.";

        private readonly IThreadRepositroy threadRepositroy;
        private readonly ISecurityServices forumSecurity;
        private readonly IThreadMapper threadMapper;
        private readonly IUserServices userServices;
        private readonly IReplyService replyService;

        public ThreadService(IThreadRepositroy threadRepositroy,
                            ISecurityServices securityServices,
                            IThreadMapper threadMapper,
                            IUserServices userServices,
                            IReplyService replyService)
        {
            this.threadRepositroy = threadRepositroy;
            this.forumSecurity = securityServices;
            this.threadMapper = threadMapper;
            this.userServices = userServices;
            this.replyService = replyService;
        }

        public ThreadResponseDto Create(ThreadCreateDto threadCreateDto, int loggedUserId)
        {
            var loggedUser = this.userServices.GetById(loggedUserId);
            var newThread = this.threadMapper.Map(threadCreateDto, loggedUser);
            var createdThread = this.threadRepositroy.Create(newThread);
            var threadResponseDto = this.threadMapper.Map(createdThread);
            return threadResponseDto;
        }

        public ThreadResponseDto Update(int id, ThreadUpdateDto threadUpdateDto, int loggedUserId)
        {
            var threadToUpdate = this.threadRepositroy.GetById(id);            
            var mappedThread = this.threadMapper.Map(threadToUpdate, threadUpdateDto);
            var updatedThread = this.threadRepositroy.Update(threadToUpdate, mappedThread);
            var resultThread = this.threadMapper.Map(updatedThread);
            return resultThread;
        }

        public ThreadResponseDto Delete(int id, int loggedUserId)
        {
            var loggedUser = userServices.GetById(loggedUserId);
            var threadToDelete = this.threadRepositroy.GetById(id);
    
            if (threadToDelete.Author.Equals(loggedUser) || loggedUser.IsAdmin)
            {
                var deletedThread = this.threadRepositroy.Delete(threadToDelete);
                var mappedThread = this.threadMapper.Map(deletedThread);

                return mappedThread;
            }

            throw new UnauthorizedOperationException(UnauthorizedErrorMessage);
        }

        public List<ThreadResponseDto> GetAll()
        {
            var allThreads = this.threadRepositroy.GetAll();
            var result = this.threadMapper.Map(allThreads);          
            return result;
        }

        public ThreadResponseDto GetById(int id)
        {
            var thread = this.threadRepositroy.GetById(id);            
            var threadResponseDto = this.threadMapper.Map(thread);
            return threadResponseDto;
        }

        public List<ThreadResponseDto> GetAllByUserId(int id)
        {
            var userThreads = this.threadRepositroy.GetAllByUserId(id);
            var mappedUserThreads = this.threadMapper.Map(userThreads);
            return mappedUserThreads;
        }    
    }
}
