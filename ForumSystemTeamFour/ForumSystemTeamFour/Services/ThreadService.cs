using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static ForumSystemTeamFour.Models.Enums.VoteType;

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
        private readonly ITagServices tagServices;

        public ThreadService(IThreadRepositroy threadRepositroy,
                            ISecurityServices securityServices,
                            IThreadMapper threadMapper,
                            ITagMapper tagMapper,
                            IUserServices userServices,
                            IReplyService replyService,
                            ITagServices tagServices)
        {
            this.threadRepositroy = threadRepositroy;
            this.forumSecurity = securityServices;
            this.threadMapper = threadMapper;
            this.userServices = userServices;
            this.replyService = replyService;
            this.tagMapper = tagMapper;
            this.tagServices = tagServices;
        }

        public ShortThreadResponseDto Create(ThreadCreateDto threadCreateDto, int loggedUserId)
        {
            var loggedUser = this.userServices.GetById(loggedUserId);
            var newThread = this.threadMapper.Map(threadCreateDto, loggedUser);
            var createdThread = this.threadRepositroy.Create(newThread);
            var threadResponseDto = this.threadMapper.Map(createdThread);
            if (threadCreateDto.Tags !=null)
            {
                foreach (var tag in threadCreateDto.Tags)
                {
                    var tagResponse = this.tagServices.Create(tag);
                    threadResponseDto.Tags.Add(tagResponse.Name);
                }
            }
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
            return result;
        }

        public List<ThreadVM> GetAllVM()
        {
            var allThreads = this.threadRepositroy.GetAll();
            var result = this.threadMapper.MapVMList(allThreads);
            return result;
        }

        public ThreadVM Details(int id)
        {
            var thread = this.threadRepositroy.Details(id);            
            var threadResponseDto = this.threadMapper.MapVM(thread);
            return threadResponseDto;
        }  
        
        public List<string> GetAllTags() 
        {
            var allTags = this.tagServices.GetAll();
            var listOfTags = this.tagMapper.Map(allTags);
            return listOfTags;
        }
		public ShortThreadResponseDto UpVote(int id, int loggedUserId)
		{
			// Check if the user has already voted
			var replyToUpVote = threadRepositroy.Details(id);
			var loggedUser = userServices.GetById(loggedUserId);
			if (AlreadyVoted(replyToUpVote, loggedUser.Username, out Vote vote))
			{
				if (vote.VoteType == Like)
				{
					//throw new DuplicateEntityException($"User: {vote.VoterUsername} already upvoted reply with id: {id}.");
					threadRepositroy.RemoveVote(id, loggedUser.Username);
				}
				else
				{
					threadRepositroy.ChangeVote(id, loggedUser.Username);
				}
			}
			else
			{
				replyToUpVote = threadRepositroy.UpVote(id, loggedUser.Username);
			}

			var shortThreadResponseDto = this.threadMapper.Map(replyToUpVote);
			return shortThreadResponseDto;
		}
		public ShortThreadResponseDto DownVote(int id, int loggedUserId)
		{
			// Check if the user has already voted
			var replyToDownVote = threadRepositroy.Details(id);
			var loggedUser = userServices.GetById(loggedUserId);
			if (AlreadyVoted(replyToDownVote, loggedUser.Username, out Vote vote))
			{
				if (vote.VoteType == Dislike)
				{
					//throw new DuplicateEntityException($"User: {vote.VoterUsername} already upvoted reply with id: {id}.");
					threadRepositroy.RemoveVote(id, loggedUser.Username);
				}
				else
				{
					threadRepositroy.ChangeVote(id, loggedUser.Username);
				}
			}
			else
			{
				replyToDownVote = threadRepositroy.DownVote(id, loggedUser.Username);
			}

			var shortThreadResponseDto = threadMapper.Map(replyToDownVote);
			return shortThreadResponseDto;
		}
		public VotesDto GetReplyVotes(int id)
		{
			var thread = threadRepositroy.Details(id);
			var votesDto = new VotesDto()
			{
				Likes = thread.Votes.Where(v => v.VoteType == Like).Select(u => u.VoterUsername).ToList(),
				Dislikes = thread.Votes.Where(v => v.VoteType == Dislike).Select(u => u.VoterUsername).ToList()
			};
			return votesDto;
		}
		private bool AlreadyVoted(Thread reply, string loggedUserName, out Vote vote)
		{
			vote = reply.Votes.FirstOrDefault(v => v.VoterUsername == loggedUserName);

			if (vote != null)
			{
				return true;
			}

			return false;
		}
	

		public int GetCount()
		{
			return this.threadRepositroy.GetCount();
		}
	}
}
