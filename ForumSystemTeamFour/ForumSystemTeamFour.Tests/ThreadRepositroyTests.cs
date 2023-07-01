using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Tests.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ForumSystemTeamFour.Tests.TestData.TestModels;

namespace ForumSystemTeamFour.Tests
{

    [TestClass]
    public class ThreadRepositroyTests
    {
        private static ForumDbContext TestContext;
        private static int NextTestId = 1;
        public ThreadRepositroyTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: "ForumTestDB")
                .Options;

            TestContext = new ForumDbContext(dbContextOptions);
        }

        [TestMethod]
        public void Create_ShouldThrow_WhenIdIsDuplicate() 
        {
            //Arrange
            var testRepository = new ThreadRepository(TestContext);
            var firstThread = GetTestThread();
            var secondThread = GetTestThread();

            //Act
            var createdThread = testRepository.Create(firstThread);

            //Assert

            Assert.ThrowsException<DuplicateEntityException>(() => testRepository.Create(secondThread));



        }
    }
}
