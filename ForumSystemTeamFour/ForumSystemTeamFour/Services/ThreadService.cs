﻿using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
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
        private readonly ITagMapper tagMapper;
        private readonly IUserServices userServices;
        private readonly IReplyService replyService;

        public ThreadService(IThreadRepositroy threadRepositroy,
                            ISecurityServices securityServices,
                            IThreadMapper threadMapper,
                            ITagMapper tagMapper,
                            IUserServices userServices,
                            IReplyService replyService)
        {
            this.threadRepositroy = threadRepositroy;
            this.forumSecurity = securityServices;
            this.threadMapper = threadMapper;
            this.userServices = userServices;
            this.replyService = replyService;
            this.tagMapper= tagMapper;
        }

        public ShortThreadResponseDto Create(ThreadCreateDto threadCreateDto, int loggedUserId)
        {
            var loggedUser = this.userServices.GetById(loggedUserId);
            var newThread = this.threadMapper.Map(threadCreateDto, loggedUser);
            var createdThread = this.threadRepositroy.Create(newThread);
            var threadResponseDto = this.threadMapper.Map(createdThread);
            return threadResponseDto;
        }

        public ShortThreadResponseDto Update(int id, ThreadUpdateDto threadUpdateDto, int loggedUserId)
        {
            var threadToUpdate = this.threadRepositroy.Details(id);            
            var mappedThread = this.threadMapper.Map(threadToUpdate, threadUpdateDto);
            var updatedThread = this.threadRepositroy.Update(threadToUpdate, mappedThread);
            var resultThread = this.threadMapper.Map(updatedThread);
            return resultThread;
        }

        public ShortThreadResponseDto Delete(int id, int loggedUserId)
        {
            var loggedUser = userServices.GetById(loggedUserId);
            var threadToDelete = this.threadRepositroy.Details(id);
    
            if (threadToDelete.Author.Equals(loggedUser) || loggedUser.IsAdmin)
            {
                var deletedThread = this.threadRepositroy.Delete(threadToDelete);
                var mappedThread = this.threadMapper.Map(deletedThread);

                return mappedThread;
            }

            throw new UnauthorizedOperationException(UnauthorizedErrorMessage);
        }


        public PaginatedList<ShortThreadResponseDto> FilterBy(int loggedUserId, ThreadQueryParameters filterParameters)
        {
            var loggedUser = this.userServices.GetById(loggedUserId);            
            var filteredThreads = this.threadRepositroy.FilterBy(loggedUser, filterParameters);
            
            return this.threadMapper.Map(filteredThreads);
        }        

        public List<ShortThreadResponseDto> GetAll()
        {
            var allThreads = this.threadRepositroy.GetAll();
            var result = this.threadMapper.Map(allThreads);
            int a = 1;
            return result;
        }

        public ShortThreadResponseDto Details(int id)
        {
            var thread = this.threadRepositroy.Details(id);            
            var threadResponseDto = this.threadMapper.Map(thread);
            return threadResponseDto;
        }        
    }
}
