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
                   var mockTagMapper = new Mock<ITagMapper>();
                var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockTagMapper.Object,
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
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                   var mockTagMapper = new Mock<ITagMapper>();
                var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockTagMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);
            int idToUpdate = DefaultId;
                    int loggedUserId = DefaultId;
                 var threadUpdateDto = TestModels.GetTestThreadUpdateDto();

                   var updatedThread = testedServices
                                        .Update(idToUpdate, threadUpdateDto,loggedUserId);

            Assert.AreEqual(threadUpdateDto.Title, updatedThread.Title);
            Assert.AreEqual(threadUpdateDto.Content, updatedThread.Content);
        }

        [TestMethod]
        public void Delete_ShouldDeleteThread_WhenInputIsValid()
        {
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                   var mockTagMapper = new Mock<ITagMapper>();
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();
                var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockTagMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            int IdtoDelete = TestModels.DefaultId;
                    int loggedUserId = TestModels.DefaultId;
                   var defaultThread = TestModels.GetTestThreadResponseDto();

                    mockUserServices.Setup(service => service
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Returns(TestModels
                                    .GetDefaultUser());

                  var deletedThread = testedServices
                                    .Delete(IdtoDelete, loggedUserId);

            Assert.AreEqual(defaultThread.Title, deletedThread.Title);
            Assert.AreEqual(defaultThread.Content, deletedThread.Content);
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenUnauthorized()
        {
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                   var mockTagMapper = new Mock<ITagMapper>();
                var mockThreadMapper = TestModels.GetTestThreadMapper();
            var mockThreadRepository = TestModels.GetTestThreadRepositroy();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockTagMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            int IdtoDelete2 = TestModels.DefaultId;
                    int loggedUserId = TestModels.DefaultId;
           var nonAdminNonAuthorUser = TestModels.GetUser();

                    mockUserServices.Setup(service => service
                                    .GetById(It
                                    .IsAny<int>()))
                                    .Returns(nonAdminNonAuthorUser);

            Assert.ThrowsException<UnauthorizedOperationException>(() => testedServices
                                    .Delete(loggedUserId, IdtoDelete2));
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllThreads_WhenCalled()
        {
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                   var mockTagMapper = new Mock<ITagMapper>();
                var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockTagMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            var allThreads = testedServices.GetAll();            

            Assert.AreEqual(3, allThreads.Count);
            Assert.IsInstanceOfType(allThreads, typeof(List<ShortThreadResponseDto>));
        }     

        [TestMethod]
        public void GetById_ShouldReturnThread_WhenIdIsValid()
        {
                var mockUserServices = new Mock<IUserServices>();
                var mockReplyService = new Mock<IReplyService>();
            var mockThreadRepository = new Mock<IThreadRepositroy>();
            var mockSecurityServices = new Mock<ISecurityServices>();
                   var mockTagMapper = new Mock<ITagMapper>();
                var mockThreadMapper = TestModels.GetTestThreadMapper();

            var testedServices = new ThreadService(mockThreadRepository.Object,
                                                    mockSecurityServices.Object,
                                                    mockThreadMapper.Object,
                                                    mockTagMapper.Object,
                                                    mockUserServices.Object,
                                                    mockReplyService.Object);

            int threadId = TestModels.DefaultId;
                      var threadToComape = TestModels.GetTestThreadResponseDto();

                              var thread = testedServices
                                        .Details(threadId);

            Assert.AreEqual(threadToComape.Title, thread.Title);
            Assert.AreEqual(threadToComape.Content, thread.Content);
        }       
    }
}
