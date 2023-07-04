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

            var createdThread = testRepository.Create(newThread);

            Assert.AreEqual(newThread, createdThread);
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
            var testRepository = new ThreadRepository(TestContext);
            var listOfThreads = TestModels.GetTestThreads(3);

            TestContext.Threads.Add(listOfThreads[0]);
            TestContext.Threads.Add(listOfThreads[1]);
            TestContext.Threads.Add(listOfThreads[2]);
            Save();

            var allThreads = testRepository.GetAll();

            Assert.AreEqual(3, allThreads.Count);
            CollectionAssert.Contains(allThreads, listOfThreads[0]);
            CollectionAssert.Contains(allThreads, listOfThreads[1]);
            CollectionAssert.Contains(allThreads, listOfThreads[2]);
        }


        [TestMethod]
        public void GetAllShould_Throw_WhenNoThreads()
        {
            var testRepository = new ThreadRepository(TestContext);

            Assert.ThrowsException<EntityNotFoundException>(() => testRepository
                                .GetAll());

        }

        [TestMethod]
        public void GetById_ShouldReturnThread_WhenThreadExists()
        {
            var testRepository = new ThreadRepository(TestContext);
            var testThread = TestModels.GetTestDefaultThread();
            TestContext.Threads.Add(testThread);
            Save();

            var retrievedThread = testRepository.GetById(testThread.Id);

            Assert.IsNotNull(retrievedThread);
            Assert.AreEqual(testThread.Id, retrievedThread.Id);
            Assert.AreEqual(testThread.Title, retrievedThread.Title);
            Assert.AreEqual(testThread.Content, retrievedThread.Content);
        }

        [TestMethod]
        public void GetById_ShouldThrowEntityNotFoundException_WhenThreadDoesNotExist()
        {
            var testRepository = new ThreadRepository(TestContext);
            var nonExistentThreadId = 2;

            Assert.ThrowsException<EntityNotFoundException>(() => testRepository.GetById(nonExistentThreadId));
        }
        [TestMethod]
        public void GetAllByUserId_ShouldReturnThreads_WhenUserIdIsValid()
        {
            var threadRepository = new ThreadRepository(TestContext);
            var testUser = TestModels.GetTestUser(TestModels.DefaultId);
            var testThreads = TestModels.GetTestThreads(3);

            foreach (var thread in testThreads)
            {
                thread.AuthorId = testUser.Id;
                thread.Author = testUser;
                TestContext.Threads.Add(thread);
            }
            Save();

            var resultThreads = threadRepository.GetAllByUserId(testUser.Id);

            Assert.AreEqual(testThreads.Count, resultThreads.Count);

            foreach (var thread in resultThreads)
            {
                Assert.AreEqual(testUser.Id, thread.AuthorId);
            }
        }

        [TestMethod]
        public void GetAllByUserId_ShouldThrowEntityNotFoundException_WhenUserDoesNotHaveAnyThreads()
        {
            var threadRepository = new ThreadRepository(TestContext);
            var testUser = TestModels.GetTestUser(TestModels.DefaultId);
            TestContext.Users.Add(testUser);
            Save();

            Assert.ThrowsException<EntityNotFoundException>(() => threadRepository.GetAllByUserId(testUser.Id));
        }
        [TestMethod]
        public void Update_ShouldUpdateThread_WhenInputIsValid()
        {
            var threadRepository = new ThreadRepository(TestContext);
            var testThread = TestModels.GetTestThread();
            TestContext.Threads.Add(testThread);
            Save();

            var updatedThread = GetTestThread();
                updatedThread.Title = "UpdatedTitle";
                updatedThread.Content = "UpdatedContent";

            var resultThread = threadRepository.Update(testThread, updatedThread);

            Assert.AreEqual(updatedThread.Title, resultThread.Title);
            Assert.AreEqual(updatedThread.Content, resultThread.Content);
        }
        public void Save()
        {
            TestContext.SaveChanges();
        }
    }
}




