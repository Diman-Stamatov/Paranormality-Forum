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
            : base(options) { }
        public ForumDbContext()
            : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThreadTag> ThreadTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var users = new List<User>() 
            {
                new User {
                Id = 1,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "cGFzc3dvcmRPbmU=", //passwordOne in plain text
                IsAdmin = true 
                },
                new User {
                Id = 2,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "cGFzc3dvcmRUd28K", //passwordTwo in plain text
                IsBlocked = true 
                },
                new User {
                Id = 3,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "cGFzc3dvcmRUaHJlZQo=" //passwordThree in plain text etc.
                },
                new User {
                Id = 4,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "cGFzc3dvcmRGb3VyCg=="
                },
                new User {
                Id = 5,
                FirstName = "FirstNameFive",
                LastName = "LastNameFive",
                Username = "UsernameFive",
                Email = "FirstnameFive@Lastname.com",
                Password = "cGFzc3dvcmRGaXZl"
                }
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
                new Thread {
                Id = 1,
                AuthorId = 2,
                Title = "First lol",
                CreationDate = DateTime.Now.AddMinutes(1),
                Content = "Hey guys, check out this cool new forum I found!"
                },
                new Thread {
                Id = 2,
                AuthorId = 1,
                Title = "Welcome to Paranormality.",
                CreationDate = DateTime.Now.AddMinutes(2),
                Content = "This is not a forum for the faint of heart." +
                            " If you need something to get started with, see the pinned threads for some basic resources." +
                            " We hope you enjoy your venture into the spooks, the creeps and the unknown."
                }
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

            modelBuilder.Entity<Thread>()
                .HasMany<Tag>(thread => thread.Tags)
                .WithMany(tag => tag.Threads)
                .UsingEntity<ThreadTag>();


            var replies = new List<Reply>()
            {
                 new Reply {
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
                },
                new Reply{
                Id = 2,
                AuthorId = 1,
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(4),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                }
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


            var tags = new List<Tag>()
            {
                new Tag{ Id = 1, Name = "##ModPost"},
                new Tag{ Id = 2, Name = "Ufo"},
                new Tag{ Id = 3, Name = "Skinwalker"},
                new Tag{ Id = 4, Name = "Bigfoot"}
            };
            modelBuilder.Entity<Tag>().HasData(tags);

            modelBuilder.Entity<Tag>()
               .HasMany<Thread>(tag => tag.Threads)
               .WithMany(thread => thread.Tags)
               .UsingEntity<ThreadTag>();


            var threadTags = new List<ThreadTag>()
            {
                new ThreadTag(){ThreadId = 1, TagId = 2},
                new ThreadTag(){ThreadId = 1, TagId = 3},
                new ThreadTag(){ThreadId = 1, TagId = 4},
                new ThreadTag(){ThreadId = 2, TagId = 1}
            };
            modelBuilder.Entity<ThreadTag>().HasData(threadTags);






        }
    }
}
