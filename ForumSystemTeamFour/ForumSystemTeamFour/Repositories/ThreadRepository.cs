﻿using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                            .Where(thread => !thread.IsDeleted)
                            .Include(thread => thread.Replies)
                            .Include(thread => thread.Author)
                            .ToList();

            return threads;
        }

        public Thread GetById(int id)
        {
                    var thread = this.context.Threads
                            .Where(t => !t.IsDeleted)
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
