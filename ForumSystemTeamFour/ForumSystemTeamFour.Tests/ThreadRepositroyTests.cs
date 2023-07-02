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

using static ForumSystemTeamFour.Tests.TestData.TestModels;

namespace ForumSystemTeamFour.Tests
{

    [TestClass]
    public class ThreadRepositroyTests
    {
        private static ForumDbContext TestContext;

        public ThreadRepositroyTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: "ForumTestThreadsDB")
                .Options;

            TestContext = new ForumDbContext(dbContextOptions);
        }

        [TestMethod]
        public void Create_ShouldCreateThread_WhenInputIsValid()
        {
            var testRepository = new ThreadRepository(TestContext);
            var newThread = GetTestThread();

            var createdUser = testRepository.Create(newThread);

            Assert.AreEqual(newThread, createdUser);
        }

        [TestMethod]
        public void Create_ShouldThrow_WhenIdIsDuplicate()
        {
            //Arrange
            var testRepository = new ThreadRepository(TestContext);
            var firstThread = GetTestThread();
            var secondThread = GetTestThread();
            secondThread.Id = DefaultId;
            var createdThread = testRepository.Create(firstThread);

            //Act & Assert

            Assert.ThrowsException<DuplicateEntityException>(() => testRepository.Create(secondThread));
        }

        [TestMethod]
        public void Delete_ShouldDelete_WhenInputIsValid()
        {
            var testRepository = new ThreadRepository(TestContext);
            var threadToDelete = GetTestThread();

            var deletedThread = testRepository.Delete(threadToDelete);

            Assert.AreEqual(true, deletedThread.IsDeleted);
        }

        [TestMethod]
        public void GetAll_ShoulReturn_AllThread()
        {
            var testRepository = new ThreadRepository(TestContext);
            var testThreadOne = GetTestThread();
            var testThreadTwo = GetTestThread();
            var testThreadThree = GetTestThread();

            var allThread = testRepository.GetAll();

            Assert.AreEqual(true, allThread.Count != 0);
        }

    }
}
