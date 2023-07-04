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
            var mockUserServices = new Mock<IUserServices>();
            var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>();
            var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

           var testCreateDto = TestModels.GetTestThreadCreateDto();
            var defaultThread = TestModels.GetTestThreadResponseDto();
  


            var createdThread = testedServices
                            .Create(testCreateDto, DefaultId);

            Assert.AreEqual(defaultThread.Title, createdThread.Title);
            Assert.AreEqual(defaultThread.Content, createdThread.Content);
        }

        [TestMethod]
        public void Update_ShouldUpdateThread_WhenInputIsValid()
        {           
            var mockThreadRepository = new Mock<IThreadRepositroy>().Object;
            var mockSecurityServices = new Mock<ISecurityServices>().Object;
                var mockUserServices = new Mock<IUserServices>().Object;
                var mockReplyService = new Mock<IReplyService>().Object;
                var mockThreadMapper = TestModels.GetTestThreadMapper().Object;

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
        public void Delete_ShouldDeleteThread_WhenInputIsValid()
        {
            var mockSecurityServices = new Mock<ISecurityServices>();
            var mockReplyService = new Mock<IReplyService>();
            var mockUserServices = new Mock<IUserServices>();
            var mockThreadMapper = TestModels.GetTestThreadMapper();
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);
            int IdtoDelete = TestModels.DefaultId;
            int loggedUserId = TestModels.DefaultId;

            mockUserServices.Setup(service => service
                            .GetById(It
                            .IsAny<int>()))
                            .Returns(TestModels
                            .GetDefaultUser());

            var deletedThread = testedServices.Delete(IdtoDelete, loggedUserId);
            var defaultThread = TestModels.GetTestThreadResponseDto();

            Assert.AreEqual(defaultThread.Title, deletedThread.Title);
            Assert.AreEqual(defaultThread.Content, deletedThread.Content);
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenUnauthorized()
        {
            // Arrange
            var mockSecurityServices = new Mock<ISecurityServices>();
                var mockReplyService = new Mock<IReplyService>();
                var mockUserServices = new Mock<IUserServices>();
                var mockThreadMapper = TestModels.GetTestThreadMapper();
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);
            int IdtoDelete2 = TestModels.DefaultId;
            int loggedUserId = TestModels.DefaultId;

            var nonAdminNonAuthorUser = TestModels.GetUser();

            mockUserServices.Setup(service => service
                            .GetById(It
                            .IsAny<int>()))
                            .Returns(nonAdminNonAuthorUser);

            //Act & Assert
            Assert.ThrowsException<UnauthorizedOperationException>(() => testedServices
                                    .Delete(loggedUserId, IdtoDelete2));
        }




        [TestMethod]
        public void GetAll_ShouldReturnAllThreads_WhenCalled()
        {
            var mockUserServices = new Mock<IUserServices>();
            var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>(); ;
            var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            var allThreads = testedServices.GetAll();
            var allTreadsToCompare = TestModels.GetTestListOfUserThreadResponseDto(3);

            Assert.AreEqual(3, allThreads.Count);
            Assert.IsInstanceOfType(allThreads, typeof(List<ThreadResponseDto>));
        }     

        [TestMethod]
        public void GetById_ShouldReturnThread_WhenIdIsValid()
        {
                var mockThreadRepository = new Mock<IThreadRepositroy>();
                var mockSecurityServices = new Mock<ISecurityServices>();
                    var mockUserServices = new Mock<IUserServices>();
                    var mockReplyService = new Mock<IReplyService>();
                    var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);
            int threadId = TestModels.DefaultId;

            var thread = testedServices.GetById(threadId);

            Assert.AreEqual(TestModels.GetTestThreadResponseDto().Title, thread.Title);
            Assert.AreEqual(TestModels.GetTestThreadResponseDto().Content, thread.Content);
        }

        [TestMethod]
        public void GetAllByUserId_ShouldReturnListOfThreads()
        {
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);
            int threadId = TestModels.DefaultId;

            var thread = testedServices.GetAllByUserId(threadId);

            Assert.AreEqual(3, thread.Count);
            Assert.IsInstanceOfType(thread, typeof(List<ThreadResponseDto>));
        }
    }
}
