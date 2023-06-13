using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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

            var users = new List<User>()
            {
                new User{Id = 1,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "passwordOne",
                IsAdmin = true
                },
                new User{Id = 2,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "passwordTwo"
                },
                new User{Id = 3,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "passwordThree"
                },
                new User{Id = 4,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "passwordFour"
                },
                new User{Id = 5,
                FirstName = "FirstNameFive",
                LastName = "LastNameFive",
                Username = "UsernameFive",
                Email = "FirstnameFive@Lastname.com",
                Password = "passwordFive"
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            var tags = new List<Tag>()
            {
                new Tag{Id = 1, Name = "Ufo"},
                new Tag{Id = 2, Name = "Skinwalker"},
                new Tag{Id = 3, Name = "Bigfoot"},
            };
            modelBuilder.Entity<Tag>().HasData(tags);

        }
    }
}
