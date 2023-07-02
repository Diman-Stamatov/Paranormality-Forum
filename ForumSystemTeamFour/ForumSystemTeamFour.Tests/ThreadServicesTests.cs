using ForumSystemTeamFour.Services.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Tests.TestData;
using Moq;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ForumSystemTeamFour.Tests.TestData.TestModels;
using Castle.Components.DictionaryAdapter.Xml;

namespace ForumSystemTeamFour.Tests
{
    [TestClass]
    public class ThreadServicesTests
    {
        [TestMethod]
        public void Create_ShouldCreateThread_WhenInputIsValid()
        {
            // Arrange
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = new Mock<IUserServices>();
            var mockReplyService = new Mock<IReplyService>();

            var testedServices = new ThreadService(mockThreadRepository, mockSecurityServices, mockThreadMapper, mockUserServices.Object, mockReplyService.Object);
            var testCreateDto = TestModels.GetTestThreadCreateDto();
            var defaultThread = TestModels.GetTestThreadResponseDto();

            mockUserServices.Setup(service => service.GetById(It.IsAny<int>())).Returns(TestModels.GetDefaultUser());

            // Act
            var createdThread = testedServices.Create(testCreateDto, TestModels.DefaultId);

            // Assert
            Assert.AreEqual(defaultThread.Title, createdThread.Title);
            Assert.AreEqual(defaultThread.Content, createdThread.Content);
        }
        [TestMethod]
        public void Update_ShouldUpdateThread_WhenInputIsValid()
        {
            // Arrange
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = new Mock<IUserServices>().Object;
            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository, mockSecurityServices, mockThreadMapper, mockUserServices, mockReplyService);

            int idToUpdate = TestModels.DefaultId;
            var threadUpdateDto = TestModels.GetTestThreadUpdateDto();
            int loggedUserId = TestModels.DefaultId;

            // Act
            var updatedThread = testedServices.Update(idToUpdate, threadUpdateDto, loggedUserId);

            // Assert
            Assert.AreEqual(threadUpdateDto.Title, updatedThread.Title);
            Assert.AreEqual(threadUpdateDto.Content, updatedThread.Content);
        }
        [TestMethod]
        public void Update_ShouldThrow_WhenThreadIsDeleted()
        {
            // Arrange
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
            mockThreadRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(() =>
                {
                    var thread = TestModels.GetTestThread();
                    thread.IsDeleted = true;
                    return thread;
                });

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = new Mock<IUserServices>().Object;
            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object, mockSecurityServices, mockThreadMapper, mockUserServices, mockReplyService);

            int idToUpdate = TestModels.DefaultId;
            var threadUpdateDto = TestModels.GetTestThreadUpdateDto();
            int loggedUserId = TestModels.DefaultId;

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.Update(idToUpdate, threadUpdateDto, loggedUserId));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenThreadNotFound()
        {
            // Arrange
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
            mockThreadRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = new Mock<IUserServices>().Object;
            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object, mockSecurityServices, mockThreadMapper, mockUserServices, mockReplyService);
            int idToDelete = TestModels.DefaultId;
            int loggedUserId = TestModels.DefaultId;

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.Delete(idToDelete, loggedUserId));
        }
        [TestMethod]
        public void Delete_ShouldDelete_WhenInputIsValid()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = GetTestUserServices().Object;
            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices,
                                                    mockReplyService);
            User loggedUserId = GetDefaultUser();
            Models.Thread IdtoDelete = GetTestThread();
            IdtoDelete.Author.Id = 1;
            var deletedThread = testedServices.Delete(IdtoDelete.Author.Id, loggedUserId.Id);
            var defaultThread = deletedThread;

            Assert.AreEqual(defaultThread, deletedThread);
        }

        //[TestMethod]
        //public void Delete_ShouldDelete_WhenAdminIsLogged()
        //{
        //    var mockUserServices = new Mock<IUserServices>();
        //    mockUserServices.Setup(s => s.GetById(It.IsAny<int>())).Returns(TestModels.GetDefaultAdmin());
        //    var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
        //    var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
        //    var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
        //    var mockReplyService = new Mock<IReplyService>().Object;

        //    var testedServices = new ThreadService(mockThreadRepository,
        //                                            mockSecurityServices,
        //                                            mockThreadMapper,
        //                                            mockUserServices,
        //                                            mockReplyService);
        //    User loggedUserId = GetDefaultUser();
        //    Models.Thread IdtoDelete = GetTestThread();
        //    IdtoDelete.Author.Id = 1;
        //    var deletedThread = testedServices.Delete(IdtoDelete.Author.Id, loggedUserId.Id);
        //    var defaultThread = deletedThread;

        //    Assert.AreEqual(defaultThread, deletedThread);
        //}

        [TestMethod]
        public void Delete_ShouldThrow_WhenLoggedUserNotFound()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
            mockThreadRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = new Mock<IUserServices>().Object;
            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object, mockSecurityServices, mockThreadMapper, mockUserServices, mockReplyService);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.Delete(loggedUserId, IdtoDelete));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenThreadToDeleteNotFound()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
            mockThreadRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
            var mockUserServices = new Mock<IUserServices>().Object;
            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object, mockSecurityServices, mockThreadMapper, mockUserServices, mockReplyService);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.Delete(loggedUserId, IdtoDelete));
        }
        [TestMethod]
        public void Delete_ShouldThrow_WhenLoggedUserIsNotAuthorized()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockThreadMapper = TestModels.GetTestThreadMapper().Object;

            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(s => s.GetById(It.IsAny<int>())).Returns(TestModels.GetUser());

            var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository, mockSecurityServices, mockThreadMapper, mockUserServices.Object, mockReplyService);
            User loggedUserId = TestModels.GetDefaultUser();
            Models.Thread IdtoDelete = TestModels.GetTestThread();
            IdtoDelete.Author.Id = 2;

            Assert.ThrowsException<UnauthorizedOperationException>(() => testedServices.Delete(loggedUserId.Id, IdtoDelete.Id));
        }
        //[TestMethod]
        //public void GetAll_ShouldReturn_AllThreads()
        //{
        //    // Arrange
        //    var mockThreadRepository = TestModels.GetTestThreadRepositroy();
        //    var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
        //    var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
        //    var mockUserServices = new Mock<IUserServices>().Object;
        //    var mockReplyService = new Mock<IReplyService>().Object;

        //    var testThreadOne = TestModels.GetTestThread();
        //    var testThreadTwo = TestModels.GetTestThread();
        //    var testThreadThree = TestModels.GetTestThread();

        //    var testSequence = new List<Models.Thread>() { testThreadOne, testThreadTwo, testThreadThree };

        //    mockThreadRepository.Setup(repo => repo.GetAll()).Returns(testSequence);

        //    var testedServices = new ThreadService(mockThreadRepository.Object, mockSecurityServices, mockThreadMapper, mockUserServices, mockReplyService);

        //    // Act
        //    var allThreads = testedServices.GetAll();

        //    // Assert
        //    Assert.AreEqual(3, allThreads.Count);
        //    Assert.AreEqual(testThreadOne.Title, allThreads[0].Title);
        //    Assert.AreEqual(testThreadTwo.Title, allThreads[1].Title);
        //    Assert.AreEqual(testThreadThree.Title, allThreads[2].Title);
        //}
    }


}