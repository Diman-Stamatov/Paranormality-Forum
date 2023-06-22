using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Tests.MockModels;
using Moq;

namespace ForumSystemTeamFour.Tests
{
    [TestClass]
    public class UserServicesTests
    {
        /*[DataRow(TaskTitleMaxLength + 1)]*/
        
        [TestMethod]        
        public void Block_ShouldBlockUser_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserID = TestModels.DefaultId;
            int idToBlock = TestModels.DefaultId + 1;

            var blockedUser = testedServices.Block(loggedUserID, idToBlock);

            Assert.AreEqual(blockedUser.Blocked, true);

        }

        [TestMethod]
        public void Block_ShouldThrow_WhenNotAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserID = TestModels.DefaultId;
            int idToBlock = TestModels.DefaultId + 1;

            Assert.ThrowsException<UnauthorizedAccessException>(()=> testedServices.Block(loggedUserID, idToBlock));

        }

        [TestMethod]
        public void Block_ShouldThrow_WhenUserNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository=>repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserID = TestModels.DefaultId;
            int idToBlock = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(()=> testedServices.Block(loggedUserID, idToBlock));

        }

        [TestMethod]
        public void Block_ShouldThrow_WhenUserUserToBlockIsAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserID = TestModels.DefaultId;
            int idToBlock = TestModels.DefaultId + 1;

            Assert.ThrowsException<InvalidUserInputException>(()=> testedServices.Block(loggedUserID, idToBlock));

        }

        [TestMethod]
        public void Block_ShouldThrow_WhenUserUserToBlockIsBlocked()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserID = TestModels.DefaultId;
            int idToBlock = TestModels.DefaultId + 1;

            Assert.ThrowsException<InvalidUserInputException>(()=> testedServices.Block(loggedUserID, idToBlock));

        }

        [TestMethod]
        public void Create_ShouldCreate_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            var testCreateDto = TestModels.GetTestUserCreateDto();
            var defaultUser = TestModels.GetDefaultUser();

            var createdUser = testedServices.Create(testCreateDto);
            Assert.AreEqual(defaultUser, createdUser);
        }

        [TestMethod]
        public void Create_ShouldThrow_WhenEmailIsDuplicate()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.Create(It.IsAny<User>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            var testCreateDto = TestModels.GetTestUserCreateDto();
            var defaultUser = TestModels.GetDefaultUser();

            Assert.ThrowsException<DuplicateEntityException>(()=> testedServices.Create(testCreateDto));
        }

        [TestMethod]
        public void Create_ShouldThrow_WhenUsernameIsDuplicate()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.Create(It.IsAny<User>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            var testCreateDto = TestModels.GetTestUserCreateDto();
            var defaultUser = TestModels.GetDefaultUser();

            Assert.ThrowsException<DuplicateEntityException>(()=> testedServices.Create(testCreateDto));
        }

        [TestMethod]
        public void Delete_ShouldDelete_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId + 1;

            var deletedUser = testedServices.Delete(loggedUserId, IdtoDelete);
            var defaultUser = TestModels.GetDefaultUser();
            Assert.AreEqual(defaultUser, deletedUser);
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenLoggedUserNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(()=> testedServices.Delete(loggedUserId, IdtoDelete));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenUserToDeleteNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(()=> testedServices.Delete(loggedUserId, IdtoDelete));
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenLoggedUserIsNotAuthorized()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDelete = TestModels.DefaultId + 1;

            Assert.ThrowsException<UnauthorizedAccessException>(()=> testedServices.Delete(loggedUserId, IdtoDelete));
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldDemote_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDemote = TestModels.DefaultId + 1;

            var demotedUser = testedServices.DemoteFromAdmin(loggedUserId, IdtoDemote);
            
            Assert.AreEqual(demotedUser.IsAdmin, false);
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDemote = TestModels.DefaultId + 1;

            Assert.ThrowsException<UnauthorizedAccessException>(()=> testedServices.DemoteFromAdmin(loggedUserId, IdtoDemote));
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldThrow_WhenUserToDemoteIsNotAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.DemoteFromAdmin(It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int IdtoDemote = TestModels.DefaultId + 1;

            Assert.ThrowsException<InvalidUserInputException>(()=> testedServices.DemoteFromAdmin(loggedUserId, IdtoDemote));
        }
    }
}