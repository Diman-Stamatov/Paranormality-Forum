using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ForumSystemTeamFour.Exceptions;
using static ForumSystemTeamFour.Models.Enums.VoteType; 

namespace ForumSystemTeamFour.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IRepliesRepository repository;
        private readonly IReplyMapper replyMapper;
        private readonly IUserServices userServices;
        private readonly ISecurityServices securityServices;
        private readonly IThreadRepositroy threadRepositroy;
        public ReplyService(IRepliesRepository replyRepository, 
                            IReplyMapper replyMapper, 
                            IUserServices userServices, 
                            ISecurityServices securityServices,
                            IThreadRepositroy threadRepositroy)
        {
            this.repository = replyRepository;
            this.replyMapper = replyMapper;
            this.userServices = userServices;
            this.securityServices = securityServices;
            this.threadRepositroy = threadRepositroy;
        }

        public ReplyReadDto Create(ReplyCreateDto replyCreateDto, int loggedUserId)
        {
            var loggedUser = userServices.GetById(loggedUserId);

            if (ParentThreadExists(replyCreateDto.ThreadId.Value))
            {
                var replyToCreate = replyMapper.Map(replyCreateDto, loggedUser);

                Reply reply = repository.Create(replyToCreate);

                ReplyReadDto replyDto = replyMapper.Map(reply);
                return replyDto;
            }
            throw new EntityNotFoundException($"Thread with id:{replyCreateDto.ThreadId} does not exist.");
        }
        public List<ReplyReadDto> FilterBy(ReplyQueryParameters filterParameters)
        {
            var replies = repository.FilterBy(filterParameters);
            List<ReplyReadDto> replyDtoList = replyMapper.Map(replies);
            return replyDtoList;
        }

        public ReplyReadDto GetById(int id)
        {
            var reply = repository.GetById(id);
            ReplyReadDto replyDto = replyMapper.Map(reply);
            return replyDto;
        }
        public List<ReplyReadDto> GetByThreadId(int id)
        {
            ReplyQueryParameters filterByThreadId = new ReplyQueryParameters()
            {
                ThreadId = id
            };
            return FilterBy(filterByThreadId);
        }

        public ReplyReadDto Update(int id, ReplyUpdateDto replyUpdateDto, int loggedUserId)
        {
            // Check if the user is owner
            var loggedUser = userServices.GetById(loggedUserId);
            var replyToUpdate = repository.GetById(id);
            securityServices.CheckAuthorAuthorization(loggedUser, replyToUpdate);

            // TODO: Check if the parent thread exists, maybe????

            Reply reply = replyMapper.Map(replyToUpdate, replyUpdateDto);
            var updatedReply = repository.Update(id, reply);

            ReplyReadDto replyDto = replyMapper.Map(updatedReply);
            return replyDto;
        }
        public ReplyReadDto Delete(int id, int loggedUserId)
        {
            // Check if the user is owner of the reply
            var loggedUser = userServices.GetById(loggedUserId);
            var replyToUpdate = repository.GetById(id);
            securityServices.CheckAuthorAuthorization(loggedUser, replyToUpdate);

            Reply replyToDelete = repository.Delete(id);

            ReplyReadDto replyDto = replyMapper.Map(replyToDelete);

            return replyDto;
        }
        public ReplyReadDto UpVote(int id, int loggedUserId)
        {
            // Check if the user has already voted
            var replyToUpVote = repository.GetById(id);
            var loggedUser = userServices.GetById(loggedUserId);
            if (AlreadyVoted(replyToUpVote, loggedUser.Username, out Vote vote))
            {
                if (vote.VoteType == Like)
                {
                    throw new DuplicateEntityException($"User: {vote.VoterUsername} already upvoted reply with id: {id}.");
                }
                else
                {
                    repository.ChangeVote(id, loggedUser.Username);
                }
            }
            else
            {
                replyToUpVote = repository.UpVote(id, loggedUser.Username);
            }

            ReplyReadDto replyDto = replyMapper.Map(replyToUpVote);
            return replyDto;
        }
        public ReplyReadDto DownVote(int id, int loggedUserId)
        {
            // Check if the user has already voted
            var replyToDownVote = repository.GetById(id);
            var loggedUser = userServices.GetById(loggedUserId);
            if (AlreadyVoted(replyToDownVote, loggedUser.Username, out Vote vote))
            {
                if (vote.VoteType == Dislike)
                {
                    throw new DuplicateEntityException($"User: {vote.VoterUsername} already downvoted reply with id: {id}.");
                }
                else
                {
                    repository.ChangeVote(id, loggedUser.Username);
                }
            }
            else
            {
                replyToDownVote = repository.DownVote(id, loggedUser.Username);
            }

            ReplyReadDto replyDto = replyMapper.Map(replyToDownVote);
            return replyDto;
        }
        public VotesDto GetReplyVotes(int id)
        {
            var reply = repository.GetById(id);
            var votesDto = new VotesDto()
            {
                Likes = reply.Votes.Where(v => v.VoteType == Like).Select(u => u.VoterUsername).ToList(),
                Dislikes = reply.Votes.Where(v => v.VoteType == Dislike).Select(u => u.VoterUsername).ToList()
            };
            return votesDto;
        }
        private bool AlreadyVoted(Reply reply, string loggedUserName, out Vote vote)
        {
            vote = reply.Votes.FirstOrDefault(v => v.VoterUsername == loggedUserName);

            if (vote != null)
            {
                return true;
            }

            return false;
        }
        private bool ParentThreadExists(int id)
        {
            var thread = threadRepositroy.GetById(id);
            
            if(thread != null)
            {
                return true;
            }
            return false;
        }
    }
}
