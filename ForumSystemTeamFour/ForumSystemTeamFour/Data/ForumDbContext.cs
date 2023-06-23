using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Data

{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) 
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var tag1 = new Tag
            {
                Id = 1,
                Name = "##ModPost"
            };
            var tag2 = new Tag
            {
                Id = 2,
                Name = "Ufo"
            };
            var tag3 = new Tag
            {
                Id = 3,
                Name = "Skinwalker"
            };
            var tag4 = new Tag
            {
                Id = 4,
                Name = "Bigfoot"
            };

            var user1 = new User
            {
                Id = 1,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "passwordOne",                
                IsAdmin = true
            };
            var user2 = new User
            {
                Id = 2,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "passwordTwo"
            };
            var user3 = new User
            {
                Id = 3,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "passwordThree"
            };
            var user4 = new User
            {
                Id = 4,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "passwordFour"
            };
            var user5 = new User
            {
                Id = 5,
                FirstName = "FirstNameFive",
                LastName = "LastNameFive",
                Username = "UsernameFive",
                Email = "FirstnameFive@Lastname.com",
                Password = "passwordFive"
            };

            var thread1 = new Thread
            {
                Id = 1,
                AuthorId = 2,
                Title = "First lol",
                CreationDate = DateTime.Now.AddMinutes(1),                
                Content = "Hey guys, check out this cool new forum I found!"

            };
            var thread2 = new Thread
            {
                Id = 2,
                AuthorId = 1,
                Title = "Welcome to Paranormality.",
                CreationDate = DateTime.Now.AddMinutes(2),                
                Content = "This is not a forum for the faint of heart." +
                            " If you need something to get started with, see the pinned threads for some basic resources." +
                            " We hope you enjoy your venture into the spooks, the creeps and the unknown."
            };

            var reply1 = new Reply
            {
                Id = 1,
                AuthorId = 1,
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(3),                
                Content = "Some of these lists are still a work in progress, as of this writing." +
                "\n\nFilm Recommendations" +
                "\n\nGame Recommendations" +
                "\n\nRadio Shows/Podcasts" +
                "\n\nWebsites of Interest" +
                "\n\nWikipedia Articles" +
                "\n\nYouTube Videos & Channels"
            };
            var reply2 = new Reply
            {
                Id = 2,
                AuthorId = 1,
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(4),                
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions" +
                "• For everything else, refer to global and thread-specific rules."
            };

            var tags = new List<Tag>()
            {
                tag1,
                tag2,
                tag3,
                tag4
            };
            modelBuilder.Entity<Tag>().HasData(tags);

            
            var users = new List<User>() 
            {
                user1, 
                user2, 
                user3, 
                user4, 
                user5
            };            
            modelBuilder.Entity<User>().HasData(users);

            modelBuilder.Entity<User>()
                .HasMany<Thread>(user => user.Threads)
                .WithOne(thread => thread.Author)
                .HasForeignKey(thread => thread.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany<Reply>(user => user.Replies)
                .WithOne(reply => reply.Author)
                .HasForeignKey(reply => reply.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

           
            var threads = new List<Thread>()
            {
                thread1,
                thread2
            };
            modelBuilder.Entity<Thread>().HasData(threads);

            modelBuilder.Entity<Thread>()
                .HasOne<User>(thread => thread.Author)
                .WithMany(author => author.Threads)
                .HasForeignKey(thread => thread.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Thread>()
                .HasMany<Reply>(thread => thread.Replies)
                .WithOne(reply => reply.Thread)
                .HasForeignKey(reply => reply.ThreadId)
                .OnDelete(DeleteBehavior.NoAction);


            var replies = new List<Reply>()
            {
                reply1,
                reply2
            };
            modelBuilder.Entity<Reply>().HasData(replies);

            modelBuilder.Entity<Reply>()
                .HasOne<User>(reply => reply.Author)
                .WithMany(author => author.Replies)
                .HasForeignKey(reply => reply.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reply>()
                .HasOne<Thread>(reply => reply.Thread)
                .WithMany(thread => thread.Replies)
                .HasForeignKey(reply => reply.ThreadId)
                .OnDelete(DeleteBehavior.NoAction);





            

            
        }
    }
}
