using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Repositories
{
    public class RepliesRepository : IRepliesRepository
    {
        private readonly ForumDbContext context;

        public RepliesRepository(ForumDbContext context)
        {
            this.context = context;
        }

        public Reply Create(Reply reply)
        {
            context.Replies.Add(reply);
            context.SaveChanges();

            return reply;
        }
        public Reply GetById(int id)
        {
            Reply reply = context.Replies.Where(r => r.Id == id).FirstOrDefault();

            return reply ?? throw new EntityNotFoundException($"Reply with id={id} doesn't exist.");
        }

        public List<Reply> FilterBy(ReplyQueryParameters filterParameters)
        {
            IEnumerable<Reply> replies = context.Replies;
            List<Reply> result = new List<Reply>();

            if (!string.IsNullOrEmpty(filterParameters.UserName))
            {
                result.AddRange(FilterByUserName(replies, filterParameters.UserName));
            }
            else if (!string.IsNullOrWhiteSpace(filterParameters.Email))
            {
                result.AddRange(FilterByEmail(replies, filterParameters.Email));
            }

            if (filterParameters.CreationDate.HasValue)
            {
                result.AddRange(FilterByCreationDate(replies, filterParameters.CreationDate.Value));
            }

            return result;
        }
        private IEnumerable<Reply> FilterByUserName(IEnumerable<Reply> replies, string? userName)
        {
            return replies.Where(r => r.Author.Username == userName);
        }
        private IEnumerable<Reply> FilterByEmail(IEnumerable<Reply> replies, string? email)
        {
            return replies.Where(r => r.Author.Email == email);
        }
        private IEnumerable<Reply> FilterByCreationDate(IEnumerable<Reply> replies, DateTime date)
        {
            string datePattern = "yyyy-MM-dd";
            return replies.Where(r => r.CreationDate.ToString(datePattern) == date.ToString(datePattern));
        }
        public Reply Update(int id, Reply reply)
        {
            var replyToUpdate = GetById(id);
            replyToUpdate.Content = reply.Content;
            replyToUpdate.CreationDate = DateTime.Now;

            context.Replies.Update(replyToUpdate);
            context.SaveChanges();

            return replyToUpdate;
        }
        public Reply Delete(int id)
        {
            var replyToRemove = GetById(id);
            context.Replies.Remove(replyToRemove);
            context.SaveChanges();

            return replyToRemove;
        }

        public Reply UpVote(int id)
        {
            var replyToUpVote = GetById(id);
            replyToUpVote.Likes += 1;

            context.Replies.Update(replyToUpVote);
            context.SaveChanges();

            return replyToUpVote;
        }
        public Reply DownVote(int id)
        {
            var replyToDownVote = GetById(id);
            replyToDownVote.Likes -= 1;

            context.Replies.Update(replyToDownVote);
            context.SaveChanges();

            return replyToDownVote;
        }

        // TODO: Add soting and ordering
    }
}
