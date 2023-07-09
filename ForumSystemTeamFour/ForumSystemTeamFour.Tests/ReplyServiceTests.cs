using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Interfaces;
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
        public void Create_ShouldReturnDro_WhenInputIsValid()
        {
            // Arrange
            var expectedResult = GetTestReplyReadDto();
            var replyCreateDto = GetTestReplyCreateDto();

            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.Create(replyCreateDto, DefaultId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void Create_ShouldThrow_WhenParentThreadNotFound()
        {
            // Arrange
            var replyCreateDto = GetTestReplyCreateDto();

            var mockThreadRepository = new Mock<IThreadRepositroy>();
            mockThreadRepository.Setup(repository => repository.Details(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var mockRepliesRepository = GetTestRepliesRepository();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository.Object);
            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Create(replyCreateDto, DefaultId), $"Thread with id:{replyCreateDto.ThreadId} does not exist.");
            mockRepliesRepository.Verify(repository => repository.Create(It.IsAny<Reply>()), Times.Never);
        }
        [TestMethod]
        public void Create_ShouldThrow_WhenAuthorNotFound()
        {
            // Arrange
            var replyCreateDto = GetTestReplyCreateDto();

            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(service => service.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();
            var mockRepliesRepository = GetTestRepliesRepository();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices.Object, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Create(replyCreateDto, DefaultId));
            mockRepliesRepository.Verify(repository => repository.Create(It.IsAny<Reply>()), Times.Never);
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

            var mockRepliesRepository = GetTestRepliesRepository();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices.Object, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Update(DefaultReplyId, updateDto, DefaultId));
            mockRepliesRepository.Verify(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()), Times.Never);
        }
        [TestMethod]
        public void Update_ShouldThrow_WhenReplyNotFound()
        {
            // Arrange
            var mockRepliesRepository = new Mock<IRepliesRepository>();

            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();
            mockRepliesRepository.Setup(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()))
                .Returns(GetTestReply());


            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Update(DefaultReplyId, It.IsAny<ReplyUpdateDto>(), DefaultId));
            mockRepliesRepository.Verify(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()), Times.Never);
        }
        [TestMethod]
        public void Update_ShouldThrow_WhenUserNotAuthorized()
        {
            // Arrange
            var mockRepliesRepository = GetTestRepliesRepository();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockInvalidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => sut.Update(DefaultReplyId, It.IsAny<ReplyUpdateDto>(), DefaultId));
            mockRepliesRepository.Verify(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()), Times.Never);
        }
        [TestMethod]
        public void Delete_ShouldDeleteRecord_WhenInputIsValid()
        {
            // Arrange
            var expectedResult = GetTestReplyReadDto();

            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.Delete(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void Delete_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var mockRepliesRepository = GetTestRepliesRepository();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices.Object, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Delete(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.Delete(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void Delete_ShouldThrow_WhenReplyNotFound()
        {
            // Arrange
            var mockRepliesRepository = new Mock<IRepliesRepository>();

            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();
            mockRepliesRepository.Setup(repository => repository.Delete(It.IsAny<int>()))
                .Returns(GetTestReply());


            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.Delete(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.Delete(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void Delete_ShouldThrow_WhenUserNotAuthorized()
        {
            // Arrange
            var mockRepliesRepository = GetTestRepliesRepository();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockInvalidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => sut.Delete(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.Delete(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void UpVote_ShouldReturnReplyReadDto_WhenInputIsValid()
        {
            // Arrange
            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.UpVote(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(ReplyReadDto));
        }
        [TestMethod]
        public void UpVote_ShouldReturnCorrectNumberOfVotes_WhenInputIsValid()
        {
            // Arrange
            var expectedResult = GetTestReplyReadDto();
            expectedResult.Likes += 1;

            var mockReplyMapper = new Mock<IReplyMapper>();
            mockReplyMapper.Setup(mapper => mapper.Map(It.IsAny<Reply>()))
                .Returns(GetTestUpVotedReplyReadDto());

            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper.Object, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.UpVote(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(actualResult, expectedResult);
        }
        [TestMethod]
        public void UpVote_ShouldThrow_WhenReplyNotFound()
        {
            // Arrange
            var mockRepliesRepository = new Mock<IRepliesRepository>();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();
            
            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.UpVote(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.UpVote(It.IsAny<int>(),It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void UpVote_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var mockRepliesRepository = GetTestRepliesRepository();
            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(service => service.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices.Object, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.UpVote(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.UpVote(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void UpVote_ShoudThrow_WhenAlreadyVoted()
        {
            // Arrange
            var loggedUserId = DefaultId;
            var upvotedReply = GetTestUpVotedReply();

            var mockRepliesRepository = GetTestRepliesRepository();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(upvotedReply);

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<DuplicateEntityException>(() => sut.UpVote(upvotedReply.Id, loggedUserId));
            mockRepliesRepository.Verify(repository => repository.UpVote(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void UpVote_ShoudChangeVote_WhenAlreadyDownVoted()
        {
            // Arrange
            var loggedUserId = DefaultId;
            var downvotedReply = GetTestDownVotedReply();

            var expectedResult = GetTestUpVotedReplyReadDto();

            var mockRepliesRepository = GetTestRepliesRepository();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(downvotedReply);

            var mockReplyMapper = new Mock<IReplyMapper>();
            mockReplyMapper.Setup(mapper => mapper.Map(It.IsAny<Reply>()))
                .Returns(GetTestUpVotedReplyReadDto());

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper.Object, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.UpVote(downvotedReply.Id,loggedUserId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepliesRepository.Verify(repository => repository.ChangeVote(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(1));
        }
        [TestMethod]
        public void DownVote_ShouldReturnReplyReadDto_WhenInputIsValid()
        {
            // Arrange
            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.DownVote(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(ReplyReadDto));
        }

        [TestMethod]
        public void DownVote_ShouldReturnCorrectNumberOfVotes_WhenInputIsValid()
        {
            // Arrange
            var expectedResult = GetTestReplyReadDto();
            expectedResult.Dislikes += 1;

            var mockReplyMapper = new Mock<IReplyMapper>();
            mockReplyMapper.Setup(mapper => mapper.Map(It.IsAny<Reply>()))
                .Returns(GetTestDownVotedReplyReadDto());

            var sut = new ReplyService(mockRepliesRepository, mockReplyMapper.Object, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.DownVote(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(actualResult, expectedResult);
        }
        [TestMethod]
        public void DownVote_ShouldThrow_WhenReplyNotFound()
        {
            // Arrange
            var mockRepliesRepository = new Mock<IRepliesRepository>();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.DownVote(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.DownVote(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void DownVote_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var mockRepliesRepository = GetTestRepliesRepository();
            var mockUserServices = new Mock<IUserServices>();
            mockUserServices.Setup(service => service.GetById(It.IsAny<int>()))
                .Throws<EntityNotFoundException>();

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices.Object, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<EntityNotFoundException>(() => sut.DownVote(It.IsAny<int>(), It.IsAny<int>()));
            mockRepliesRepository.Verify(repository => repository.DownVote(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void DownVote_ShoudThrow_WhenAlreadyVoted()
        {
            // Arrange
            var loggedUserId = DefaultId;
            var downvotedReply = GetTestDownVotedReply();

            var mockRepliesRepository = GetTestRepliesRepository();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(downvotedReply);

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act and Assert
            Assert.ThrowsException<DuplicateEntityException>(() => sut.DownVote(downvotedReply.Id, loggedUserId));
            mockRepliesRepository.Verify(repository => repository.DownVote(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void DownVote_ShoudChangeVote_WhenAlreadyUpVoted()
        {
            // Arrange
            var loggedUserId = DefaultId;
            var upvotedReply = GetTestUpVotedReply();

            var expectedResult = GetTestDownVotedReplyReadDto();

            var mockRepliesRepository = GetTestRepliesRepository();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(upvotedReply);

            var mockReplyMapper = new Mock<IReplyMapper>();
            mockReplyMapper.Setup(mapper => mapper.Map(It.IsAny<Reply>()))
                .Returns(GetTestDownVotedReplyReadDto());

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper.Object, mockUserServices, mockValidSecurityServices, mockThreadRepository);

            // Act
            var actualResult = sut.DownVote(upvotedReply.Id, loggedUserId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepliesRepository.Verify(repository => repository.ChangeVote(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(1));
        }
        [TestMethod]
        public void GetVotes_ShouldReturnVotes_WhenInputIsValid()
        {
            // Arrange
            var expectedResult = new VotesDto()
            {
                Likes = new List<string>() { ValidUsername },
                Dislikes = new List<string>() { "ValidUserName2", "ValidUserName3" }
            };

            var mockRepliesRepository = new Mock<IRepliesRepository>();
            mockRepliesRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(GetTestUpVotedReply());

            var sut = new ReplyService(mockRepliesRepository.Object, mockReplyMapper, mockUserServices, mockValidSecurityServices, mockThreadRepository);
            // Act
            var actualResult = sut.GetReplyVotes(DefaultReplyId);
            actualResult.Dislikes.Add("ValidUserName2");
            actualResult.Dislikes.Add("ValidUserName3");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
