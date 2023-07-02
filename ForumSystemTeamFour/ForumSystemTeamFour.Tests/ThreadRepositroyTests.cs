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
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Tests
{

    [TestClass]
    public class ThreadRepositroyTests
    {
        private static ForumDbContext TestContext;

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: "ForumTestDB")
                .Options;

            TestContext = new ForumDbContext(dbContextOptions);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestContext.Database.EnsureDeleted();
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
            secondThread.Id = firstThread.Id;

            var createdThread = testRepository.Create(firstThread);

            //Act & Assert

            Assert.ThrowsException<DuplicateEntityException>(()
                => testRepository.Create(secondThread));
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
        public void GetAll_ShouldReturnAllThreads_WhenCalled()
        {
            // Arrange
            var testRepository = new ThreadRepository(TestContext);
            var testThreadOne = TestModels.GetTestThread();
            var testThreadTwo = TestModels.GetTestThread();
            var testThreadThree = TestModels.GetTestThread();

            TestContext.Threads.Add(testThreadOne);
            TestContext.Threads.Add(testThreadTwo);
            TestContext.Threads.Add(testThreadThree);
            TestContext.SaveChanges();

            // Act
            var allThreads = testRepository.GetAll();

            // Assert
            Assert.AreEqual(3, allThreads.Count);
            CollectionAssert.Contains(allThreads, testThreadOne);
            CollectionAssert.Contains(allThreads, testThreadTwo);
            CollectionAssert.Contains(allThreads, testThreadThree);
        }

        [TestMethod]
        public void GetById_ShouldReturnThread_WhenThreadExists()
        {
            // Arrange
            var testRepository = new ThreadRepository(TestContext);
            var testThread = TestModels.GetTestThread();
            TestContext.Threads.Add(testThread);
            TestContext.SaveChanges();

            // Act
            var retrievedThread = testRepository.GetById(testThread.Id);

            // Assert
            Assert.IsNotNull(retrievedThread);
            Assert.AreEqual(testThread.Id, retrievedThread.Id);
            Assert.AreEqual(testThread.Title, retrievedThread.Title);
            Assert.AreEqual(testThread.Content, retrievedThread.Content);
        }

        [TestMethod]
        public void GetById_ShouldThrowEntityNotFoundException_WhenThreadDoesNotExist()
        {
            // Arrange
            var testRepository = new ThreadRepository(TestContext);
            var nonExistentThreadId = 999;

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => testRepository.GetById(nonExistentThreadId));
        }
        [TestMethod]
        public void GetAllByUserId_ShouldReturnThreads_WhenUserIdIsValid()
        {
            // Arrange
            var threadRepository = new ThreadRepository(TestContext);
            var testUser = TestModels.GetTestUser(TestModels.DefaultId);
            var testThreads = TestModels.GetTestThreads(3);
            foreach (var thread in testThreads)
            {
                thread.AuthorId = testUser.Id;
                thread.Author = testUser;
                TestContext.Threads.Add(thread);
            }
            TestContext.SaveChanges();

            // Act
            var resultThreads = threadRepository.GetAllByUserId(testUser.Id);

            // Assert
            Assert.AreEqual(testThreads.Count, resultThreads.Count);
            foreach (var thread in resultThreads)
            {
                Assert.AreEqual(testUser.Id, thread.AuthorId);
            }
        }

        [TestMethod]
        public void GetAllByUserId_ShouldThrowEntityNotFoundException_WhenUserDoesNotHaveAnyThreads()
        {
            // Arrange
            var threadRepository = new ThreadRepository(TestContext);
            var testUser = TestModels.GetTestUser(TestModels.DefaultId);
            TestContext.Users.Add(testUser);
            TestContext.SaveChanges();

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => threadRepository.GetAllByUserId(testUser.Id));
        }
        [TestMethod]
        public void Update_ShouldUpdateThread_WhenInputIsValid()
        {
            // Arrange
            var threadRepository = new ThreadRepository(TestContext);
            var testThread = TestModels.GetTestThread();
            TestContext.Threads.Add(testThread);
            TestContext.SaveChanges();

            var updatedThread = GetTestThread();
            updatedThread.Title = "UpdatedTitle";
            updatedThread.Content = "UpdatedContent";

            // Act
            var resultThread = threadRepository.Update(testThread, updatedThread);

            // Assert
            Assert.AreEqual(updatedThread.Title, resultThread.Title);
            Assert.AreEqual(updatedThread.Content, resultThread.Content);
        }
      
    }

}




