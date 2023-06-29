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
    public class ThreadService
    {
        private const string ModifyCommentErrorMessage = "Only author or admin can modify a comment.";

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

        public ThreadResponcseDto Create(ThreadCreateDto threadCreateDto, `int loggedUserId)
        {
            var loggedUser = userServices.GetById(loggedUserId);
            var newThread = this.threadMapper.Map(threadCreateDto, loggedUser);
            var createdThread = this.threadRepositroy.Create(newThread);
            var threadResponseDto = this.threadMapper.Map(createdThread);
            return threadResponseDto;
        }

        public ThreadResponcseDto Update(int id, ThreadUpdateDto threadUpdateDto, int loggedUserId)
        {
            var threadToUpdate = this.threadRepositroy.GetById(id);
            var mappedThread = this.threadMapper.Map(threadToUpdate, threadUpdateDto);
            var updatedThread = this.threadRepositroy.Update(threadToUpdate, mappedThread);
            var resultThread = this.threadMapper.Map(updatedThread);
            return resultThread;
        }

        public ThreadResponcseDto Delete(int id, int loggedUserId)
        {
            var loggedUser = userServices.GetById(loggedUserId);
            var thread = this.threadRepositroy.GetById(id);

            if(thread.IsDeleted)
            {
                throw new EntityNotFoundException($"Thread with id {id} doesn't exist!");
            }
            if (!thread.Author.Equals(loggedUser) && !loggedUser.IsAdmin)
            {
                throw new UnauthorizedOperationException(ModifyCommentErrorMessage);
            }
            thread = this.threadRepositroy.Delete(id);
            var mappedThread = this.threadMapper.Map(thread);           

            return mappedThread;
        }
    }
}
