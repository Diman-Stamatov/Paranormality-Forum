﻿using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static ForumSystemTeamFour.Models.Enums.VoteType;

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
            reply.Author.TotalPosts++;
            context.Replies.Add(reply);
            context.SaveChanges();

            return reply;
        }
        public Reply GetById(int id)
        {
            // Filter by soft deleted.
            Reply reply = context.Replies
                                .Where(r => r.Id == id && r.IsDeleted == false)
                                .Include(a => a.Author)
                                .Include(v => v.Votes)
                                .FirstOrDefault();

            return reply ?? throw new EntityNotFoundException($"Reply with id={id} doesn't exist.");
        }

        public List<Reply> FilterBy(ReplyQueryParameters filter)
        {
            // Filter for soft deleted.
            IEnumerable<Reply> result = context.Replies
                                               .Where(r => r.IsDeleted == false)
                                               .Include(a => a.Author)
                                               .Include(v => v.Votes)
                                               .ToList();

            if (filter.ThreadId !=0)
            {
                result = result.Where(reply => reply.ThreadId == filter.ThreadId);
            }
            // Filter by attributes
            result = FilterByUserAttribute(result, filter.UserName, filter.Email);

            // Filter by date
            result = FilterByCreationDate(result, filter.CreationDate);
            result = FilterByDateRange(result, filter.CreatedAfter, filter.CreatedBefore, filter.ModifiedAfter, filter.ModifiedBefore);

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
        private IEnumerable<Reply> FilterByCreationDate(IEnumerable<Reply> replies, string date)
        {
            if (DateTime.TryParse(date, out DateTime creationDate))
            {
                DateTime dateToCompare = creationDate;
                string datePattern = "yyyy-MM-dd";
                return replies.Where(r => r.CreationDate.ToString(datePattern) == dateToCompare.ToString(datePattern));
            }
            return replies;
        }
        private IEnumerable<Reply> FilterByDateRange(IEnumerable<Reply> replies, string createdAfter, string createdBefore, string modifiedAfter, string modifiedBefore)
        {
            replies = FilterByAfterDate(replies, createdAfter, d => d.CreationDate);
            replies = FilterByBeforeDate(replies, createdBefore, d => d.CreationDate);

            replies = FilterByAfterDate(replies, modifiedAfter, d => d.ModificationDate);
            replies = FilterByBeforeDate(replies, modifiedBefore, d => d.ModificationDate);

            return replies;
        }
        private IEnumerable<Reply> FilterByAfterDate(IEnumerable<Reply> replies, string after, Func<Reply,DateTime> properySelector )
        {
            if (DateTime.TryParse(after, out DateTime afterDate))
            {
                replies = replies.Where(d => properySelector(d) >= afterDate);
            }
            return replies;
        }
        private IEnumerable<Reply> FilterByBeforeDate(IEnumerable<Reply> replies, string before, Func<Reply, DateTime> properySelector)
        {
            if (DateTime.TryParse(before, out DateTime beforeDate))
            {
                replies = replies.Where(d => properySelector(d) <= beforeDate);
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
				case "likes":
					return replies.OrderBy(reply => reply.Votes.Where(vote=>vote.VoteType == VoteType.Like).Count());
				case "dislikes":
					return replies.OrderBy(reply => reply.Votes.Where(vote => vote.VoteType == VoteType.Dislike).Count());
				// The following handles null or empty strings
				default:
                    return replies;
            }
        }
        private IEnumerable<Reply> SortOrder(IEnumerable<Reply> replies, string sortOrder)
        {
            return (sortOrder.ToLower() == "desc" || sortOrder.ToLower() == "descending") ? replies.Reverse() : replies;
        }
        public Reply Update(int id, Reply reply)
        {
            var replyToUpdate = GetById(id);
            replyToUpdate.Content = reply.Content;
            replyToUpdate.ModificationDate = DateTime.Now;

            context.SaveChanges();

            return replyToUpdate;
        }
        public Reply Delete(int id)
        {
            var replyToRemove = GetById(id);
            replyToRemove.IsDeleted = true;

            context.SaveChanges();

            return replyToRemove;
        }
        public Reply UpVote(int id, string loggedUserName)
        {
            var replyToUpVote = GetById(id);
            var vote = new ReplyVote()
            {
                ReplyId = id,
                VoterUsername = loggedUserName,
                VoteType = VoteType.Like
            };
            replyToUpVote.Votes.Add(vote);

            context.SaveChanges();

            return replyToUpVote;
        }
        public Reply DownVote(int id, string loggedUserName)
        {
            var replyToDownVote = GetById(id);
            var vote = new ReplyVote()
            {
                ReplyId = id,
                VoterUsername = loggedUserName,
                VoteType = VoteType.Dislike
            };
            replyToDownVote.Votes.Add(vote);

            context.SaveChanges();

            return replyToDownVote;
        }
        public Reply ChangeVote(int id, string loggedUserName)
        {
            var replyToChangeVote = GetById(id);
            var vote = replyToChangeVote.Votes.FirstOrDefault(v => v.VoterUsername == loggedUserName);

            if (vote.VoteType == Like)
            {
                vote.VoteType = Dislike;
            }
            else
            {
                vote.VoteType = Like;
            }

            context.SaveChanges();

            return replyToChangeVote;
        }
        public Reply RemoveVote(int id, string loggedUserName)
        {
            var replyToRemoveVote = GetById(id);
            var vote = replyToRemoveVote.Votes.FirstOrDefault(v => v.VoterUsername == loggedUserName);

            replyToRemoveVote.Votes.Remove(vote);

            context.SaveChanges();

            return replyToRemoveVote;
        }

        public int GetCount()
        {
            return context.Replies.Count();
		}
    }
}
