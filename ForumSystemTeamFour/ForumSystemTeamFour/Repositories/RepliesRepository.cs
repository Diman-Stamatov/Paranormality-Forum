using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
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
            IEnumerable<Reply> result = context.Replies.ToList();

            // Filter
            result = FilterByUserAttribute(result, filterParameters.UserName, filterParameters.Email);
            result = FilterByCreationDate(result, filterParameters.CreationDate);

            // Sort and order
            if (result.Count() == 0)
            { 
                throw new EntityNotFoundException("No replies with the specified filter parameters were found.");
            }
            else
            {
                result = SortBy(result, filterParameters.SortBy);
                result = SortOrder(result, filterParameters.SortOrder);
            }

            return result.ToList();
        }

        private IEnumerable<Reply> FilterByUserAttribute(IEnumerable<Reply> replies, string userName, string email)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                return FilterByUserName(replies, userName);
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                return FilterByEmail(replies, email);
            }
            return replies;
        }
        private IEnumerable<Reply> FilterByUserName(IEnumerable<Reply> replies, string userName)
        {
            return replies.Where(r => r.Author.Username == userName);
        }
        private IEnumerable<Reply> FilterByEmail(IEnumerable<Reply> replies, string email)
        {
            return replies.Where(r => r.Author.Email == email);
        }
        private IEnumerable<Reply> FilterByCreationDate(IEnumerable<Reply> replies, DateTime? date)
        {
            if (date.HasValue)
            {
                DateTime dateToCompare = (DateTime)date;
                string datePattern = "yyyy-MM-dd";
                return replies.Where(r => r.CreationDate.ToString(datePattern) == dateToCompare.ToString(datePattern));
            }
            return replies;
        }
        private IEnumerable<Reply> SortBy(IEnumerable<Reply> replies, string sortCriteria)
        {
            switch (sortCriteria.ToLower())
            {
                case "username":
                    return replies.OrderBy(reply => reply.Author.Username);
                case "email":
                    return replies.OrderBy(reply => reply.Author.Email);
                // The following handles null or empty strings
                default:
                    return replies;
            }
        }
        private IEnumerable<Reply> SortOrder(IEnumerable<Reply> replies, string sortOrder)
        {
            return (sortOrder.ToLower() == "desc") ? replies.Reverse() : replies;
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
            replyToDownVote.Dislikes += 1;

            context.Replies.Update(replyToDownVote);
            context.SaveChanges();

            return replyToDownVote;
        }

    }
}
