using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static ForumSystemTeamFour.Models.Enums.VoteType;

namespace ForumSystemTeamFour.Repositories
{
    public class ThreadRepository : IThreadRepositroy
    {
        private const string NotFoundErrorMessage = "Thread with id {0} doesn't exist!";

        private readonly ForumDbContext context;

        public ThreadRepository(ForumDbContext context)
        {
            this.context = context;
        }

        public Thread Create(Thread thread)
        {
            thread.Author.TotalPosts++;
            context.Threads.Add(thread);
            Save();
            return thread;
        }

        public Thread Update(Thread threadToUpdate, Thread updatedThread)
        {
            threadToUpdate.Content = updatedThread.Content ?? threadToUpdate.Content;
            threadToUpdate.Title = updatedThread.Title ?? threadToUpdate.Title;
            Save();
            return threadToUpdate;
        }

        public Thread Delete(Thread thread)
        {
            thread.IsDeleted = true;
            Save();
            return thread;
        }

        public List<Thread> FilterByMV(ThreadQueryParameters filter)
        {
            // Filter for soft deleted.
            IEnumerable<Thread> result = context.Threads
                .Where(thread => thread.IsDeleted == false)
                .Include(thread => thread.Author)
                 .Include(thread => thread.Tags)
                .Include(thread => thread.Votes).ToList();

            if (filter.Tags != null && filter.Tags.Any())
            {
                foreach (var tag in filter.Tags)
                {
                    result = result
                        .Where(thread => thread.Tags.Any(t => t.Name == tag));
                  
                }
            }
            if (filter.ThreadId != 0)
            {
                result = result.Where(thread => thread.Id == filter.ThreadId);
            }
            // Filter by attributes
            result = FilterByUserAttribute(result, filter.UserName, filter.Email);

            // Filter by date
            result = FilterByCreationDate(result, filter.CreationDate);
            result = FilterByDateRange(result, filter.CreatedAfter, filter.CreatedBefore, filter.ModifiedAfter, filter.ModifiedBefore);

            // Sort and order
            if (result.Count() == 0)
            {
                throw new EntityNotFoundException("No threads with the specified filter parameters were found.");
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
        private IEnumerable<Thread> FilterByUserAttribute(IEnumerable<Thread> threads, string userName, string email)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                return FilterByUserName(threads, userName);
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                return FilterByEmail(threads, email);
            }
            return threads;
        }
        private IEnumerable<Thread> FilterByUserName(IEnumerable<Thread> threads, string userName)
        {
            return threads.Where(thread => thread.Author.Username == userName);
        }
        private IEnumerable<Thread> FilterByEmail(IEnumerable<Thread> replies, string email)
        {
            return replies.Where(r => r.Author.Email == email);
        }
        private IEnumerable<Thread> FilterByCreationDate(IEnumerable<Thread> replies, string date)
        {
            if (DateTime.TryParse(date, out DateTime creationDate))
            {
                DateTime dateToCompare = creationDate;
                string datePattern = "yyyy-MM-dd";
                return replies.Where(r => r.CreationDate.ToString(datePattern) == dateToCompare.ToString(datePattern));
            }
            return replies;
        }
        private IEnumerable<Thread> FilterByDateRange(IEnumerable<Thread> replies, string createdAfter, string createdBefore, string modifiedAfter, string modifiedBefore)
        {
            replies = FilterByAfterDate(replies, createdAfter, d => d.CreationDate);
            replies = FilterByBeforeDate(replies, createdBefore, d => d.CreationDate);

            replies = FilterByAfterDate(replies, modifiedAfter, d => d.ModificationDate);
            replies = FilterByBeforeDate(replies, modifiedBefore, d => d.ModificationDate);

            return replies;
        }
        private IEnumerable<Thread> FilterByAfterDate(IEnumerable<Thread> replies, string after, Func<Thread, DateTime> properySelector)
        {
            if (DateTime.TryParse(after, out DateTime afterDate))
            {
                replies = replies.Where(d => properySelector(d) >= afterDate);
            }
            return replies;
        }
        private IEnumerable<Thread> FilterByBeforeDate(IEnumerable<Thread> replies, string before, Func<Thread, DateTime> properySelector)
        {
            if (DateTime.TryParse(before, out DateTime beforeDate))
            {
                replies = replies.Where(d => properySelector(d) <= beforeDate);
            }
            return replies;
        }
        private IEnumerable<Thread> SortBy(IEnumerable<Thread> threads, string sortCriteria)
        {
            switch (sortCriteria.ToLower())
            {
                case "username":
                    return threads.OrderBy(thread => thread.Author.Username);
                case "email":
                    return threads.OrderBy(thread => thread.Author.Email);
                case "creationdate":
                    return threads.OrderBy(thread => thread.CreationDate);
                case "likes":
                    return threads.OrderBy(thread => thread.Votes.Where(vote => vote.VoteType == VoteType.Like).Count());
                case "dislikes":
                    return threads.OrderBy(thread => thread.Votes.Where(vote => vote.VoteType == VoteType.Dislike).Count());
                // The following handles null or empty strings
                default:
                    return threads;
            }
        }
        private IEnumerable<Thread> SortOrder(IEnumerable<Thread> threads, string sortOrder)
        {
            return (sortOrder.ToLower() == "desc" || sortOrder.ToLower() == "descending") ? threads.Reverse() : threads;
        }
        public PaginatedList<Thread> FilterBy(User loggedUser, ThreadQueryParameters filterParameters)
        {
            var filteredThreads = context.Threads
                .Where(thread => thread.IsDeleted == false)
                .Include(thread => thread.Author)
                
                .Include(thread => thread.Tags)
                .Include(thread => thread.Votes)
                .Include(thread => thread.Replies)
                .ThenInclude(reply=>reply.Votes)
                .ToList();

            if (IsNotEmpty(filterParameters.UserName))
            {
                filteredThreads = filteredThreads
                    .FindAll(thread => thread.Author.Username == filterParameters.UserName);
            }

            if (filterParameters.Tags != null && filterParameters.Tags.Any())
            {
                foreach (var tag in filterParameters.Tags)
                {
                    filteredThreads = filteredThreads
                        .Where(thread => thread.Tags.Any(t => t.Name == tag)).ToList();
                }
            }

            if (IsNotEmpty(filterParameters.FirstName))
            {
                filteredThreads = filteredThreads
                    .FindAll(user => user.Author.FirstName == filterParameters.FirstName);
            }

            if (IsNotEmpty(filterParameters.LastName))
            {
                filteredThreads = filteredThreads
                    .FindAll(user => user.Author.LastName == filterParameters.LastName);
            }
            if (IsNotEmpty(filterParameters.Email))
            {
                if (!loggedUser.IsAdmin)
                {
                    throw new UnauthorizedAccessException("Only an administrator can search by E-mail!");
                }
                filteredThreads = filteredThreads
                    .FindAll(thread => thread.Author.Email == filterParameters.Email);
            }

            if (IsNotEmpty(filterParameters.SortBy))
            {
                if (filterParameters.SortBy.Equals("firstname", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredThreads = filteredThreads.OrderBy(thread => thread.Author.FirstName).ToList();
                }
                else if (filterParameters.SortBy.Equals("lastname", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredThreads = filteredThreads.OrderBy(thread => thread.Author.LastName).ToList();
                }
                else if (filterParameters.SortBy.Equals("username", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredThreads = filteredThreads.OrderBy(thread => thread.Author.Username).ToList();
                }
            }
            if (IsNotEmpty(filterParameters.SortOrder) && filterParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
            {
                filteredThreads.Reverse();
            }
            if (filteredThreads.Count == 0)
            {
                throw new EntityNotFoundException("No thread correspond to the specified search parameters!");
            }

            int totalPages = (filteredThreads.Count() + 1) / filterParameters.PageSize;

            filteredThreads = Paginate(filteredThreads, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<Thread>(filteredThreads, totalPages, filterParameters.PageNumber);

        }

        public static List<Thread> Paginate(List<Thread> filteredThreads, int pageNumber, int pageSize)
        {
            return filteredThreads
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public List<Thread> GetAll()
        {
            var threads = this.context.Threads
                            .Where(thread => !thread.IsDeleted)
                            .Include(thread => thread.Replies.Where(reply => !reply.IsDeleted))
							.ThenInclude(reply => reply.Author)
							.Include(thread => thread.Author)  
                            .Include(thread => thread.Votes)
                            .ToList();
            if (threads.Count == 0 || !threads.Any())
            {
                throw new EntityNotFoundException(NotFoundErrorMessage);
            }

            return threads;
        }
		public Thread UpVote(int id, string loggedUserName)
		{
			var threadUpVote = Details(id);
			var vote = new ThreadVote()
			{
				ThreadId = id,
				VoterUsername = loggedUserName,
				VoteType = VoteType.Like
			};
			threadUpVote.Votes.Add(vote);

			context.SaveChanges();

			return threadUpVote;
		}
		public Thread DownVote(int id, string loggedUserName)
		{
			var threadToDownVote = Details(id);
			var vote = new ThreadVote()
			{
				ThreadId = id,
				VoterUsername = loggedUserName,
				VoteType = VoteType.Dislike
			};
			threadToDownVote.Votes.Add(vote);

			context.SaveChanges();

			return threadToDownVote;
		}

		public Thread ChangeVote(int id, string loggedUserName)
		{
			var threadToChangeVote = Details(id);
			var vote = threadToChangeVote.Votes.FirstOrDefault(v => v.VoterUsername == loggedUserName);

			if (vote.VoteType == Like)
			{
				vote.VoteType = Dislike;
			}
			else
			{
				vote.VoteType = Like;
			}

			context.SaveChanges();

			return threadToChangeVote;
		}

		public Thread RemoveVote(int id, string loggedUserName)
		{
			var threadToRemoveVote = Details(id);
			var vote = threadToRemoveVote.Votes.FirstOrDefault(v => v.VoterUsername == loggedUserName);

			threadToRemoveVote.Votes.Remove(vote);

			context.SaveChanges();

			return threadToRemoveVote;
		}

		public Thread Details(int id)
        {
                    var thread = this.context.Threads
                            .Where(thread => !thread.IsDeleted && thread.Id ==id)
                            .Include(thread => thread.Replies.Where(reply => !reply.IsDeleted))
                            .ThenInclude (reply => reply.Votes)
							.Include(thread => thread.Replies.Where(reply => !reply.IsDeleted))
							.ThenInclude(reply => reply.Author)
                            .Include(thread => thread.Author)	
                            .Include(thread => thread.Votes)
							.Include(thread => thread.Tags)
							.FirstOrDefault();

            return thread ?? throw new EntityNotFoundException($"Thread with id={id} doesn't exist.");
        }

        public List<Thread> GetAllByUserId(int id)
        {
            var threads = this.context.Threads
                                .Where(t => t.AuthorId == id && !t.IsDeleted)
                                .ToList();

            if (threads == null || !threads.Any())
            {
                throw new EntityNotFoundException($"Author with id={id} doesn't have any threads");
            }
            return threads;
        }
	
		private void Save()
        {
            context.SaveChanges();
        }

        private bool IsNotEmpty(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
		public int GetCount()
		{
			return context.Threads.Count();
		}

	}
}
