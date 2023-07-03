using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Services.Interfaces;
using ForumSystemTeamFour.Tests.TestData;
using Moq;
using static ForumSystemTeamFour.Tests.TestData.TestModels;

namespace ForumSystemTeamFour.Tests
{
    [TestClass]
    public class ThreadServicesTests
    {
        [TestMethod]
        public void Create_ShouldCreateThread_WhenInputIsValid()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();

            var testedServices = new ThreadService(mockThreadRepository,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

                    var testCreateDto = TestModels.GetTestThreadCreateDto();
                    var defaultThread = TestModels.GetTestThreadResponseDto();

                    mockUserServices.Setup(service => service
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Returns(TestModels.GetDefaultUser());

            var createdThread = testedServices.Create(testCreateDto, DefaultId);

            Assert.AreEqual(defaultThread.Title, createdThread.Title);
            Assert.AreEqual(defaultThread.Content, createdThread.Content);
        }

        [TestMethod]
        public void Update_ShouldUpdateThread_WhenInputIsValid()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>().Object;
                var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices,
                                                    mockReplyService);
                      int idToUpdate = DefaultId;
                    int loggedUserId = DefaultId;
                 var threadUpdateDto = TestModels.GetTestThreadUpdateDto();

            var updatedThread = testedServices.Update(idToUpdate,
                                                    threadUpdateDto,
                                                    loggedUserId);

            Assert.AreEqual(threadUpdateDto.Title, updatedThread.Title);
            Assert.AreEqual(threadUpdateDto.Content, updatedThread.Content);
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenThreadIsDeleted()
        {
                var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetById(It
                                    .IsAny<int>()))
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

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices,
                                                    mockReplyService);
                      int idToUpdate = DefaultId;
                    int loggedUserId = DefaultId;
                 var threadUpdateDto = TestModels.GetTestThreadUpdateDto();

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices
                                                    .Update(
                                                    idToUpdate,
                                                    threadUpdateDto,
                                                    loggedUserId));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenThreadNotFound()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>().Object;
                var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices,
                                                    mockReplyService);
                    int idToDelete = DefaultId;
                    int loggedUserId = DefaultId;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices
                                                    .Delete(idToDelete,
                                                    loggedUserId));
        }

        [TestMethod]
        public void Delete_ShouldDeleteThread_WhenInputIsValid()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();

            var testedServices = new ThreadService(mockThreadRepository,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId;

                    mockUserServices.Setup(service => service
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Returns(TestModels.GetDefaultUser());

            var deletedThread = testedServices.Delete(IdtoDelete, loggedUserId);
            var defaultThread = TestModels.GetTestThreadResponseDto();

            Assert.AreEqual(defaultThread.Title, deletedThread.Title);
            Assert.AreEqual(defaultThread.Content, deletedThread.Content);
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenLoggedUserNotFound()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>().Object;
                var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices,
                                                    mockReplyService);
            int loggedUserId = DefaultId;
            int IdtoDelete = DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices
                                                    .Delete(
                                                    loggedUserId,
                                                    IdtoDelete));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenThreadToDeleteNotFound()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>().Object;
                var mockReplyService = new Mock<IReplyService>().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices,
                                                    mockReplyService);
            int loggedUserId = DefaultId;
            int IdtoDelete = DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices
                                                    .Delete(
                                                    loggedUserId,
                                                    IdtoDelete));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenLoggedUserIsNotAuthorized()
        {
                var mockReplyService = new Mock<IReplyService>().Object;
                var mockUserServices = new Mock<IUserServices>();
                    mockUserServices.Setup(s => s
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Returns(TestModels
                                    .GetUser());
            var mockThreadRepository = TestModels.GetTestThreadRepositroy().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;

            var testedServices = new ThreadService(mockThreadRepository,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices.Object,
                                                    mockReplyService);
            int loggedUserId = id + 1;
            int IdtoDelete = DefaultId;

            Assert.ThrowsException<UnauthorizedOperationException>(() => testedServices
                                                    .Delete(
                                                    loggedUserId,
                                                    IdtoDelete));
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllThreads_WhenCalled()
        {
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = TestModels
                                    .GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetAll())
                                    .Returns(TestModels
                                    .GetTestThreads(3));

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            var allThreads = testedServices
                                    .GetAll();

            Assert.AreEqual(3, allThreads.Count);
            Assert.IsInstanceOfType(allThreads, typeof(List<ThreadResponseDto>));
        }
        [TestMethod]
        public void GetAll_ShouldReturnEmptyList_WhenNoThreadsExist()
        {
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetAll())
                                    .Returns(new List<Models.Thread>());
                var mockThreadMapper = new Mock<IThreadMapper>();
                    mockThreadMapper.Setup(mapper => mapper
                                    .Map(It
                                    .IsAny<List<Models.Thread>>()))
                                    .Returns(TestModels
                                    .GetTestListOfThreadResponseDto(0));


            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            Assert.ThrowsException<EntityNotFoundException>(() =>
                                                    testedServices.GetAll());
        }        

        [TestMethod]
        public void GetById_ShouldThrow_WhenThreadDoesNotExist()
        {
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                var mockThreadMapper = new Mock<IThreadMapper>();
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();

                mockThreadRepository.Setup(repository => repository
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Throws<EntityNotFoundException>();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                   mockSecurityServices.Object,
                                                   mockThreadMapper.Object,
                                                   mockUserServices.Object,
                                                   mockReplyService.Object);

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.GetById(DefaultId));
        }
        
        [TestMethod]
        public void GetAllByUserId_ShouldReturnThreads_WhenUserIdIsValid()
        {
            // Arrange
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetAllByUserId(It
                                    .IsAny<int>()))
                                    .Returns(TestModels
                                    .GetTestThreads(3));

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper();

                    var testedServices = new ThreadService(mockThreadRepository.Object,
                                                            mockSecurityServices,
                                                            mockThreadMapper.Object,
                                                            mockUserServices.Object,
                                                            mockReplyService.Object);

            int userId = DefaultId;

            // Act
            var threads = testedServices.GetAllByUserId(userId);

            // Assert
            Assert.AreEqual(3, threads.Count);
        }

        [TestMethod]
        public void GetAllByUserId_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var mockThreadRepository = GetTestThreadRepositroy();
                mockThreadRepository.Setup(repository => repository
                                    .GetAllByUserId(It
                                    .IsAny<int>()))
                                    .Throws<EntityNotFoundException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;
                var mockThreadMapper = GetTestThreadMapper().Object;
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices,
                                                    mockThreadMapper,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            int userId = DefaultId;

            // Act & Assert
            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.GetAllByUserId(userId));
        }
    }
}
