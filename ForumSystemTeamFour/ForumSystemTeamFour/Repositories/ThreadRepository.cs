using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Thread> FilterBy(User loggedUser, ThreadQueryParameters filterParameters)
        {
            var filteredThreads = context.Threads
                .Where(thread => thread.IsDeleted == false)
                .Include(thread => thread.Author)
                .Include(thread => thread.CreationDate)
                .Include(thread => thread.ModificationDate)
                .Include(thread => thread.Tags)
                .Include(thread => thread.Votes)
                .Include(thread => thread.Replies.Count)
                .ToList();

            if (IsNotEmpty(filterParameters.Author.Username))
            {
                filteredThreads = filteredThreads
                    .FindAll(thread => thread.Author.Username == filterParameters.Author.Username);
            }

            if (filterParameters.Tags != null && filterParameters.Tags.Any())
            {
                foreach (var tag in filterParameters.Tags)
                {
                    filteredThreads = filteredThreads
                        .Where(thread => thread.Tags.Any(t => t.Name == tag.Name)).ToList();
                }
            }

            if (IsNotEmpty(filterParameters.Author.FirstName))
            {
                filteredThreads = filteredThreads
                    .FindAll(user => user.Author.FirstName == filterParameters.Author.FirstName);
            }

            if (IsNotEmpty(filterParameters.Author.FirstName))
            {
                filteredThreads = filteredThreads
                    .FindAll(user => user.Author.LastName == filterParameters.Author.FirstName);
            }
            if (IsNotEmpty(filterParameters.Author.Email))
            {
                if (!loggedUser.IsAdmin)
                {
                    throw new UnauthorizedAccessException("Only an administrator can search by E-mail!");
                }
                filteredThreads = filteredThreads.FindAll(thread => thread.Author.Email == filterParameters.Author.Email);
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
            return filteredThreads;
        }

        public List<Thread> GetAll()
        {
            var threads = this.context.Threads
                            .Where(thread => !thread.IsDeleted)
                            .Include(thread => thread.Replies)
                            .Include(thread => thread.Author)                            
                            .ToList();
            if (threads.Count == 0 || !threads.Any())
            {
                throw new EntityNotFoundException(NotFoundErrorMessage);
            }

            return threads;
        }

        public Thread GetById(int id)
        {
                    var thread = this.context.Threads
                            .Where(thread => !thread.IsDeleted && thread.Id ==id)
                            .Include(thread => thread.Replies)
                            .Include(thread => thread.Author)
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

    }
}
