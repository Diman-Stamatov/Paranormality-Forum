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


            Random random = new Random();                       
            int nextUserId = 1;
            int nextThreadId = 1;
            int nextReplyId = 1;

            var users = new List<User>() 
            {
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "cGFzc3dvcmRPbmU=", //passwordOne in plain text
                IsAdmin = true 
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "cGFzc3dvcmRUd28=", //passwordTwo in plain text
                IsBlocked = true 
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "cGFzc3dvcmRUaHJlZQ==" //passwordThree in plain text etc.
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "cGFzc3dvcmRGb3Vy"
                },
                new User {
                Id = nextUserId++,
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
                Id = nextThreadId++,
                AuthorId = random.Next(1,6), //Random UserId
                Title = "First lol",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)), //random before/after 2 months in minutes  
            Content = "Hey guys, check out this cool new forum I found!"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Welcome to Paranormality.",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "This is not a forum for the faint of heart." +
                            " If you need something to get started with, see the pinned threads for some basic resources." +
                            " We hope you enjoy your venture into the spooks, the creeps and the unknown."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "How have you accepted death?No matter what happens to us did you find peace?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I'm 30 years old, while I do have plenty of time left to enjoy my life, " +
                "I'm much more aware of my mortality more than ever. I miss being a kid and a teenager, " +
                "back when I was free spirited and ignorant on this subject. I will never be satisfied with any " +
                "theories on what happens after death, I believe there is a soul and something happens to us and " +
                "that's about it. I just hope if I die of old age, it's in my sleep and it's quick. I'm just afraid " +
                "knowing it's a one way ticket and there's no refunds when my time is up. I am envious of people who have " +
                "NDEs, they come back completely different people, it's irrelevant if it's real or a hallucination."
                },
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
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Some of these lists are still a work in progress, as of this writing." +
                "\n\nFilm Recommendations" +
                "\n\nGame Recommendations" +
                "\n\nRadio Shows/Podcasts" +
                "\n\nWebsites of Interest" +
                "\n\nWikipedia Articles" +
                "\n\nYouTube Videos & Channels"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It just is. I will die sooner or later and there is nothing I can do about it. Why fret over the inevitable? I can't worry myself into immortality."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "both paths are correct\r\nboth the one that insists they're the sole path, and the one that says all paths lead to the same place\r\nthey are two sides to the same coin, and they come in go in intervals. Find the truth that feels right, that's the only way, for it is love.\r\nThere could be a pool of non-reincarnated people, deciding if either Jesus or Buddha is correct, for example. Doesn't matter"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "idk it’s gonna sound like bullshit but I understood I was going to die at 7 years old and prayed to God it would happen right then. it’s one of my fondest childhood memories.\r\n\r\ndesu I’m excited for it because it’s a total unknown. I’m in my 30s too and life has become exceedingly droll, and it already was to some extent as a 7 year old. just the same tired old spectacular tragedies playing out over and over, the world turning and turning, the sun burning and burning. it’s all so tedious. in death I place my hope that something will truly change. even if I am reincarnated or sent to hell or heaven or whatever, at least it’s not this same messy life that seems to get messier as the years separate me from my good sense\r\n\r\nit helps to accept the unknown that comes after death when you accept that you don’t actually know anything about life either; we all just hop aboard the big lie to cope"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I made sure to do (almost) all the things I wanted to do, so I wouldn’t feel regret when death comes, if for some reason it comes slowly enough to contemplate it."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Pursue the sublime OP. I used to be quite existentially dreadful and suffer but I've learned to accept through wonderful sights that there is little we know and even less we understand. There's too much weird shit going on to be just a single act by my reckoning. There's something going on between time and space and beyond time and space, and the solace I imagine in regards to my death is to be privy to the secrets that cannot be shared. Perhaps I am delusional, and perhaps God has a punchline on the otherside. For the time being, we're actors in a wonderfully strange play, and you even get to choose your parts if you're careful."
                },
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
