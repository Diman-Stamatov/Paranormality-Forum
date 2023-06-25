using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
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
        public void GetById_ShouldGetUser_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;           

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);            
            int idToGet = TestModels.DefaultId;
            var defaultUser = TestModels.GetDefaultUser();
            var foundUser = testedServices.GetById(idToGet);

            Assert.AreEqual(defaultUser, foundUser);
        }

        [TestMethod]
        public void GetById_ShouldThrow_WhenUserNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int idToGet = TestModels.DefaultId;            

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.GetById(idToGet));
        }        

        [TestMethod]        
        public void Block_ShouldBlockUser_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper();
            mockUserMapper.Setup(mapper => mapper.Map(It.IsAny<User>()))
            .Returns(TestModels.GetTestBlockedUserResponseDto());

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper.Object);
            int loggedUserID = TestModels.DefaultId;
            int idToBlock = TestModels.DefaultId + 1;

            var blockedUser = testedServices.Block(loggedUserID, idToBlock);

            Assert.AreEqual(true, blockedUser.IsBlocked);

        }

        [TestMethod]
        public void Block_ShouldThrow_WhenLoggedUserIsNotAdmin()
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
            mockUserRepository.Setup(repository=>repository.Block(It.IsAny<int>()))
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
            var defaultUser = TestModels.GetTestUserResponseDto(TestModels.DefaultId);

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
            var defaultUser = TestModels.GetTestUserResponseDto(TestModels.DefaultId);

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
            
            Assert.AreEqual(false, demotedUser.IsAdmin);
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
        public void DemoteFromAdmin_ShouldThrow_WhenUserToDemoteIsAdmin()
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

        [TestMethod]
        public void FilterBy_ShouldReturnUsers_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;            

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            var queryParameters = TestModels.GetTestUserQueryParamaters();

            var filteredUsers = testedServices.FilterBy(loggedUserId, queryParameters);

            Assert.IsInstanceOfType(filteredUsers, typeof(List<UserResponseDto>));
        }

        [TestMethod]
        public void FilterBy_ShouldThrow_WhenFilteringByEmailByNonAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.FilterBy(It.IsAny<User>(), It.IsAny<UserQueryParameters>()))
            .Throws<UnauthorizedAccessException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            var queryParameters = TestModels.GetTestUserQueryParamaters();
            queryParameters.Email = TestModels.ValidEmail;

            Assert.ThrowsException<UnauthorizedAccessException>(() => testedServices.FilterBy(loggedUserId, queryParameters));
        }

        [TestMethod]
        public void FilterBy_ShouldThrow_WhenNoUsersFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.FilterBy(It.IsAny<User>(), It.IsAny<UserQueryParameters>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            var queryParameters = TestModels.GetTestUserQueryParamaters();

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.FilterBy(loggedUserId, queryParameters));
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldPromote_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper();
            mockUserMapper.Setup(mapper => mapper.Map(It.IsAny<User>()))
            .Returns(TestModels.GetTestAdminResponseDto());

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper.Object);
            int loggedUserId = TestModels.DefaultId;
            int idToPromote = TestModels.DefaultId + 1;

            var promotedUser = testedServices.PromoteToAdmin(loggedUserId, idToPromote);

            Assert.AreEqual(true, promotedUser.IsAdmin);
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToPromote = TestModels.DefaultId + 1;

            Assert.ThrowsException<UnauthorizedAccessException>(() => testedServices.PromoteToAdmin(loggedUserId, idToPromote));
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldThrow_WhenUserNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.PromoteToAdmin(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToPromote = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.PromoteToAdmin(loggedUserId, idToPromote));
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldThrow_WhenUserIsAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.PromoteToAdmin(It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToPromote = TestModels.DefaultId + 1;

            Assert.ThrowsException<InvalidUserInputException>(() => testedServices.PromoteToAdmin(loggedUserId, idToPromote));
        }

        [TestMethod]
        public void Unblock_ShouldUnblock_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUnblock = TestModels.DefaultId + 1;

            var promotedUser = testedServices.Unblock(loggedUserId, idToUnblock);

            Assert.AreEqual(false, promotedUser.IsBlocked);
        }

        [TestMethod]
        public void Unblock_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUnblock = TestModels.DefaultId + 1;

            Assert.ThrowsException<UnauthorizedAccessException>(() => testedServices.Unblock(loggedUserId, idToUnblock));
        }

        [TestMethod]
        public void Unblock_ShouldThrow_WhenUserIsNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.Unblock(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUnblock = TestModels.DefaultId + 1;

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.Unblock(loggedUserId, idToUnblock));
        }

        [TestMethod]
        public void Unblock_ShouldThrow_WhenUserIsNotBlocked()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.Unblock(It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUnblock = TestModels.DefaultId + 1;

            Assert.ThrowsException<InvalidUserInputException>(() => testedServices.Unblock(loggedUserId, idToUnblock));
        }

        [TestMethod]
        public void Update_ShouldUpdate_WhenInputIsValid()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUpdate = TestModels.DefaultId + 1;
            var updateData = TestModels.GetTestUserUpdateDto();

            var defaultUserResponse = TestModels.GetTestUserResponseDto(TestModels.DefaultId);
            var updatedUser = testedServices.Update(loggedUserId, idToUpdate, updateData);

            Assert.AreEqual(defaultUserResponse, updatedUser);
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenUserToUpdateNotFound()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUpdate = TestModels.DefaultId + 1;
            var updateData = TestModels.GetTestUserUpdateDto();            

            Assert.ThrowsException<EntityNotFoundException>(() => testedServices.Update(loggedUserId, idToUpdate, updateData));
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenLoggedUserNotAuthorized()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository().Object;
            var mockSecurityServices = TestModels.GetInvalidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUpdate = TestModels.DefaultId + 1;
            var updateData = TestModels.GetTestUserUpdateDto();

            Assert.ThrowsException<UnauthorizedAccessException>(() => testedServices.Update(loggedUserId, idToUpdate, updateData));
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenSettingDuplicateUsername()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.Update(It.IsAny<User>(), It.IsAny<UserUpdateDto>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUpdate = TestModels.DefaultId + 1;
            var updateData = TestModels.GetTestUserUpdateDto();

            Assert.ThrowsException<DuplicateEntityException>(() => testedServices.Update(loggedUserId, idToUpdate, updateData));
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenSettingDuplicateEmail()
        {
            var mockUserRepository = TestModels.GetTestUsersRepository();
            mockUserRepository.Setup(repository => repository.Update(It.IsAny<User>(), It.IsAny<UserUpdateDto>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = TestModels.GetValidAuthenticationTestSecurity().Object;
            var mockUserMapper = TestModels.GetTestUserMapper().Object;

            var testedServices = new UserServices(mockUserRepository.Object, mockSecurityServices, mockUserMapper);
            int loggedUserId = TestModels.DefaultId;
            int idToUpdate = TestModels.DefaultId + 1;
            var updateData = TestModels.GetTestUserUpdateDto();

            Assert.ThrowsException<DuplicateEntityException>(() => testedServices.Update(loggedUserId, idToUpdate, updateData));
        }
    }
}