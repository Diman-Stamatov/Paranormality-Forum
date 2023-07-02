using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ForumSystemTeamFour.Repositories
{
    public class ThreadRepository : IThreadRepositroy
    {
        private readonly ForumDbContext context;

        public ThreadRepository(ForumDbContext context)
        {
            this.context = context;
        }

        public Thread Create(Thread thread)
        {
            CheckDublicateId(thread.Id);
            context.Threads.Add(thread);
            Save();
            return thread;
        }

        public Thread Update(Thread threadToUpdate, Thread updatedThread)
        {
            if (threadToUpdate.IsDeleted)
            {
                throw new EntityNotFoundException($"Thread with id={threadToUpdate.Id} doesn't exist.");
            }
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

        public List<Thread> GetAll()
        {
            var threads = this.context.Threads
                            .Where(t => !t.IsDeleted)
                            .Include(thread=>thread.Replies)
                            .Include(thread=>thread.Author)
                            .ToList();

            return threads;
        }

        public Thread GetById(int id)
        {
            var thread = this.context.Threads
                        .Where(t => t.Id == id && !t.IsDeleted)
                        .FirstOrDefault();

            return thread ?? throw new EntityNotFoundException($"Thread with id={id} doesn't exist.");
        }

        public List<Thread> GetAllByUserId(int id)
        {
            var threads = this.context.Threads
                            .Where(t => t.AuthorId == id && !t.IsDeleted)
                            .ToList();
            return threads ?? throw new EntityNotFoundException($"Author with id={id} doesn't have any threads");
        }
        private void Save()
        {
            context.SaveChanges();
        }

        private void CheckDublicateId(int id)
        {
            var foundThread = context.Threads.FirstOrDefault(thread => thread.Id == id);
            if (foundThread != null)
            {
                throw new DuplicateEntityException($"Thread with id {id} already exist.");
            }
        }
    }
}
