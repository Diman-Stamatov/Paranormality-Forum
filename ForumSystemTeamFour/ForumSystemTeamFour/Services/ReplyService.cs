using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IRepliesRepository repository;

        public ReplyService(IRepliesRepository replyRepository)
        {
            this.repository = replyRepository;
        }

        public Reply Create(Reply reply)
        {
           return repository.Create(reply);
        }
        public List<Reply> FilterBy(ReplyQueryParameters filterParameters)
        {
            return repository.FilterBy(filterParameters);
        }

        public Reply GetById(int id)
        {
            return repository.GetById(id);
        }

        public Reply Update(int id, Reply reply, int userId)
        {
            // TODO: Check if the user is owner
            return repository.Update(id, reply);
        }
        public Reply Delete(int id, int userId)
        {
            // TODO: Check if the user is owner of the reply
            return repository.Delete(id);
        }
        public Reply UpVote(int id)
        {
            // TODO: Check if the user has already voted
            return repository.UpVote(id);
        }

        public Reply DownVote(int id)
        {
            // TODO: Check if the user has already voted
            return repository.DownVote(id);
        }

    }
}
