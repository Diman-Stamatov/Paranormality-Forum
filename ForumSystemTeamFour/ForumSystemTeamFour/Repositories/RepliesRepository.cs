using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            Reply reply = context.Replies
                                .Where(r => r.Id == id)
                                .Include(u => u.Author)
                                .FirstOrDefault();


            return reply ?? throw new EntityNotFoundException($"Reply with id={id} doesn't exist.");
        }

        public List<Reply> FilterBy(ReplyQueryParameters filter)
        {
            IEnumerable<Reply> result = context.Replies
                                               .Include(r => r.Author)
                                               .ToList();

            // Filter
            result = FilterByUserAttribute(result, filter.UserName, filter.Email);
            if (DateTime.TryParse(filter.CreationDate, out DateTime creationDate))
            {
                result = FilterByCreationDate(result, creationDate);
            }

            // Sort and order
            if (result.Count() == 0)
            { 
                throw new EntityNotFoundException("No replies with the specified filter parameters were found.");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(filter.SortBy))
                {
                    result = SortBy(result, filter.SortBy);
                }
                if (!string.IsNullOrWhiteSpace(filter.SortOrder))
                {
                    result = SortOrder(result, filter.SortOrder);
                }
            }

            return result.ToList();
        }

        private IEnumerable<Reply> FilterByUserAttribute(IEnumerable<Reply> replies, string userName, string email)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var result = FilterByUserName(replies, userName);
                return result;
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                var result = FilterByEmail(replies, email);
                return result;
            }
            return replies;
        }
        private IEnumerable<Reply> FilterByUserName(IEnumerable<Reply> replies, string userName)
        {
            return replies.Where(r => r.Author.Username == userName).ToList();
        }
        private IEnumerable<Reply> FilterByEmail(IEnumerable<Reply> replies, string email)
        {
            return replies.Where(r => r.Author.Email == email).ToList();
        }
        private IEnumerable<Reply> FilterByCreationDate(IEnumerable<Reply> replies, DateTime? date)
        {
            if (date.HasValue)
            {
                DateTime dateToCompare = (DateTime)date;
                string datePattern = "yyyy-MM-dd";
                return replies.Where(r => r.CreationDate.ToString(datePattern) == dateToCompare.ToString(datePattern)).ToList();
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
                case "creationdate":
                    return replies.OrderBy(reply => reply.CreationDate);
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
