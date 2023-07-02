using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ForumSystemTeamFour.Services
{
    public class ThreadService : IThreadService
    {
        private const string UnauthorizedErrorMessage = "Only author or admin can modify a comment.";
        private const string NotFoundErrorMessage = "Thread with id {0} doesn't exist!";

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
            var loggedUser = userServices.GetById(loggedUserId);
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
            
            if (!threadToDelete.Author.Equals(loggedUser) && !loggedUser.IsAdmin)
            {
                throw new UnauthorizedOperationException(UnauthorizedErrorMessage);
            }
            var deletedThread= this.threadRepositroy.Delete(threadToDelete);
            var mappedThread = this.threadMapper.Map(deletedThread);           

            return mappedThread;
        }

        public List<ThreadResponseDto> GetAll()
        {
            var allThreads = this.threadRepositroy.GetAll();
            var result = this.threadMapper.Map(allThreads);
            if (result.Count == 0)
            {
                throw new EntityNotFoundException(NotFoundErrorMessage);
            }
            return result;
        }

        public ThreadResponseDto GetById(int id)
        {
            if (IsDeleted(id))
            {
                throw new EntityNotFoundException($"Thread with id={id} doesn't exist.");
            }
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

        public bool IsDeleted(int id)
        {
            ThreadResponseDto thread = GetById(id);
            return thread.isDeleted;
        }
    }
}
