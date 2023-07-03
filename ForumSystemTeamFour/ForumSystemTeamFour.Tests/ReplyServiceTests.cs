using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static ForumSystemTeamFour.Tests.TestData.TestModels;

namespace ForumSystemTeamFour.Tests
{
    [TestClass]
    public class ReplyServiceTests
    {
        private IRepliesRepository mockRepliesRepository;
        private IReplyMapper mockReplyMapper;
        private IUserServices mockUserServices;
        private ISecurityServices mockValidSecurityServices;
        private ISecurityServices mockInvalidSecurityServices;
        private IThreadRepositroy mockThreadRepository;

        [TestInitialize] 
        public void Initialize()
        {
            mockRepliesRepository = GetTestRepliesRepository().Object;
            mockReplyMapper = GetTestReplyMapper().Object;
            mockUserServices = GetTestUserServices().Object;
            mockValidSecurityServices = GetValidAuthenticationTestSecurity().Object; 
            mockInvalidSecurityServices = GetInvalidAuthenticationTestSecurity().Object;
            mockThreadRepository = GetTestThreadRepositroy().Object;
        }
        
        
        [TestMethod]
        public void GetById_ShouldReturnReplyDto_WhenInputIsValid()
        {
            // Arrange
            var expectedResult = GetTestReplyReadDto();
            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.GetById(DefaultReplyId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void FilterBy_ShouldReturnReplyDtoList_WhenInputIsValid()
        {
            // Arrange
            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            var filter = new ReplyQueryParameters()
            {
                UserName = ValidUsername
            };

            // Act
            var actualResult = sut.FilterBy(filter);

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(List<ReplyReadDto>));
        }
        [TestMethod]
        public void GetByThreadId_ShouldReturnReplyReadDtoList_WhenInputIsValid()
        {
            // Arrange
            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.GetByThreadId(ValidThreadId);

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(List<ReplyReadDto>));
        }
        [TestMethod]
        public void GetByThreadId_ShouldReturnResults_WhenInputIsValid()
        {
            // Arrange
            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.GetByThreadId(ValidThreadId);

            // Assert
            Assert.IsTrue(actualResult.Count >= 1);
        }
        [TestMethod]
        public void GetByThreadId_ShouldReturnEmptyReplyReadDtoList_WhenNoMatchIsFound()
        {
            // Arrange
            var mockReplyMapper = new Mock<IReplyMapper>();
            mockReplyMapper.Setup(mapper => mapper.Map(It.IsAny<List<Reply>>()))
                .Returns(new List<ReplyReadDto>());

            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper.Object, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.GetByThreadId(InvalidThreadId);

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(List<ReplyReadDto>));
            Assert.IsTrue(actualResult.Count == 0);
        }
        [TestMethod]
        public void Update_ShouldUpdateRecord_WhenInputIsValid()
        {
            // Arrange
            var updateTime = DateTime.Now;
            var updateContent = GetTestString(ContentMinLength, 'u');
            var updateDto = new ReplyUpdateDto()
            {
                Content = updateContent,
            };
            var expectedResult = GetTestReplyReadDto(1, DateTime.Parse("2023-01-01"), updateTime, updateContent);
            var mappedResult = GetTestReplyReadDto(1, DateTime.Parse("2023-01-01"), updateTime, updateContent);

            var mockMapper = new Mock<IReplyMapper>();
            mockMapper.Setup(Mappers => Mappers.Map(It.IsAny<Reply>()))
                .Returns(mappedResult);


            var sut = new ReplyService(mockRepliesRepository, mockMapper.Object, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.Update(DefaultReplyId, updateDto, DefaultId);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void Update_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var updateTime = DateTime.Now;
            var updateContent = GetTestString(ContentMinLength, 'u');
            var updateDto = new ReplyUpdateDto()
            {
                Content = updateContent,
            };

            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var mockRepliesRepository = new Mock<IRepliesRepository>();
            mockRepliesRepository.Setup(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()))
                .Returns(GetTestReply());

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices.Object, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            mockRepliesRepository.Verify(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()), Times.Never);
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Update(DefaultReplyId, updateDto, DefaultId));
        }
        [TestMethod]
        public void Update_ShouldThrow_WhenReplyNotFound()
        {
            // Arrange
            var updateTime = DateTime.Now;
            var updateContent = GetTestString(ContentMinLength, 'u');
            var updateDto = new ReplyUpdateDto()
            {
                Content = updateContent,
            };

            var mockRepliesRepository = new Mock<IRepliesRepository>();

            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();
            mockRepliesRepository.Setup(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()))
                .Returns(GetTestReply());


            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            mockRepliesRepository.Verify(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()), Times.Never);
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Update(DefaultReplyId, updateDto, DefaultId));
        }
        [TestMethod]
        public void Update_ShouldThrow_WhenUserNotAuthorized()
        {
            // Arrange
            var updateTime = DateTime.Now;
            var updateContent = GetTestString(ContentMinLength, 'u');
            var updateDto = new ReplyUpdateDto()
            {
                Content = updateContent,
            };

            var mockRepliesRepository = new Mock<IRepliesRepository>();

            mockRepliesRepository.Setup(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()))
                .Returns(GetTestReply());

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockInvalidSecurityServices, mockThreadRepository);

            // Act and Assert
            mockRepliesRepository.Verify(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()), Times.Never);
            Assert.ThrowsException<UnauthorizedAccessException>(() => sut.Update(DefaultReplyId, updateDto, DefaultId));
        }
    }
}
