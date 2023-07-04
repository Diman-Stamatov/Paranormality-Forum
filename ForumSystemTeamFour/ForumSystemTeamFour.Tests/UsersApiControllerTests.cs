using ForumSystemTeamFour.Services;
using static ForumSystemTeamFour.Tests.TestData.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystemTeamFour.Models.QueryParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Controllers.API;
using ForumSystemTeamFour.Exceptions;
using Moq;

namespace ForumSystemTeamFour.Tests
{
    [TestClass]
    public class UsersApiControllerTests
    {
        private void SetControllerContext(UsersApiController controller)
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("LoggedUserId", DefaultId.ToString()));
            var principal = new ClaimsPrincipal(identity);
            var context = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

            controller.ControllerContext = context;
        }

        [TestMethod]
        public void FilterUsers_ShouldGetUsers_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object; 
                       
            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            var filterParameters = new UserQueryParameters();
            
            var result = testedApi.FilterUsers(filterParameters) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;         
            Assert.AreEqual(expectedCode, result.StatusCode);             
        }

        [TestMethod]
        public void FilterUsers_ShouldThrow_WhenNoUsersFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.FilterBy(It.IsAny<int>(), It.IsAny<UserQueryParameters>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            var filterParameters = new UserQueryParameters();

            var result = testedApi.FilterUsers(filterParameters) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void FilterUsers_ShouldThrow_WhenNoUserNotAuthorized()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.FilterBy(It.IsAny<int>(), It.IsAny<UserQueryParameters>()))
            .Throws<UnauthorizedAccessException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            var filterParameters = new UserQueryParameters();

            var result = testedApi.FilterUsers(filterParameters) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateUser_ShouldCreate_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            var testCreateDto = GetTestUserCreateDto();

            var result = testedApi.CreateUser(testCreateDto) as ObjectResult;
            var expectedCode = StatusCodes.Status201Created;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateUser_ShouldThrow_WhenInputIsDuplicate()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Create(It.IsAny<UserCreateDto>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            var testCreateDto = GetTestUserCreateDto();

            var result = testedApi.CreateUser(testCreateDto) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateToken_ShouldCreate_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);
            
            string testUsername = "testLogin";
            string testPassword = "testPassword";

            var result = testedApi.CreateToken(testUsername, testPassword) as ObjectResult;
            var expectedCode = StatusCodes.Status201Created;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateToken_ShouldThrow_WhenLoginUsernameIsNotRegistered()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity();
            mockSecurityServices.Setup(services => services.CreateApiToken(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<EntityNotFoundException>();

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices.Object);
            SetControllerContext(testedApi);
            
            string testUsername = "testLogin";
            string testPassword = "testPassword";

            var result = testedApi.CreateToken(testUsername, testPassword) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateToken_ShouldThrow_WhenInputIsEmpty()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity();
            mockSecurityServices.Setup(services => services.CreateApiToken(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<InvalidUserInputException>();

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices.Object);
            SetControllerContext(testedApi);
            
            string testUsername = " ";
            string testPassword = "testPassword";

            var result = testedApi.CreateToken(testUsername, testPassword) as ObjectResult;
            var expectedCode = StatusCodes.Status400BadRequest;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Logout_ShouldLogout_WhenExecuted()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            var result = testedApi.DeleteToken() as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void UpdateUser_ShouldUpdate_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUpdate = DefaultId;
            var testUpdateData = GetTestUserUpdateDto();

            var result = testedApi.UpdateUser(idToUpdate, testUpdateData) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void UpdateUser_ShouldThrow_WhenUserNotFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserUpdateDto>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);
            
            int idToUpdate = DefaultId;
            var testUpdateData = GetTestUserUpdateDto();

            var result = testedApi.UpdateUser(idToUpdate, testUpdateData) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void UpdateUser_ShouldThrow_WhenUpdateDataIsDuplicate()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserUpdateDto>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUpdate = DefaultId;
            var testUpdateData = GetTestUserUpdateDto();

            var result = testedApi.UpdateUser(idToUpdate, testUpdateData) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void UpdateUser_ShouldThrow_WhenUserIsNotAuthorized()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserUpdateDto>()))
            .Throws<UnauthorizedAccessException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUpdate = DefaultId;
            var testUpdateData = GetTestUserUpdateDto();

            var result = testedApi.UpdateUser(idToUpdate, testUpdateData) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldPromote_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            int idtoPromote = DefaultId;
            
            var result = testedApi.PromoteToAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldThrow_WhenUserNotFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.PromoteToAdmin(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<EntityNotFoundException>();
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idtoPromote = DefaultId;

            var result = testedApi.PromoteToAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldThrow_WhenUserIsAlreadyAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.PromoteToAdmin(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<InvalidUserInputException>();
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idtoPromote = DefaultId;

            var result = testedApi.PromoteToAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void PromoteToAdmin_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.PromoteToAdmin(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<UnauthorizedAccessException>();
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idtoPromote = DefaultId;

            var result = testedApi.PromoteToAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldDemote_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToDemote = DefaultId;

            var result = testedApi.DemoteFromAdmin(idToDemote) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldThrow_WhenUserNotFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.DemoteFromAdmin(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<EntityNotFoundException>();
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idtoPromote = DefaultId;

            var result = testedApi.DemoteFromAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldThrow_WhenUserisNotAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.DemoteFromAdmin(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<InvalidUserInputException>();
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idtoPromote = DefaultId;

            var result = testedApi.DemoteFromAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DemoteFromAdmin_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.DemoteFromAdmin(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<UnauthorizedAccessException>();
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);
            
            int idtoPromote = DefaultId;

            var result = testedApi.DemoteFromAdmin(idtoPromote) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Block_ShouldBlock_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToBlock = DefaultId;

            var result = testedApi.Block(idToBlock) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }
        
        [TestMethod]
        public void Block_ShouldThrow_WhenUserNotFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Block(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToBlock = DefaultId;

            var result = testedApi.Block(idToBlock) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Block_ShouldThrow_WhenUserIsAlreadyBlocked()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Block(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToBlock = DefaultId;

            var result = testedApi.Block(idToBlock) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Block_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Block(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<UnauthorizedAccessException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToBlock = DefaultId;

            var result = testedApi.Block(idToBlock) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Unblock_ShouldUnblock_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUnblock = DefaultId;

            var result = testedApi.Unblock(idToUnblock) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Unblock_ShouldThrow_WhenUserNotFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Unblock(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUnblock = DefaultId;

            var result = testedApi.Unblock(idToUnblock) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Unblock_ShouldThrow_WhenUserIsAlreadyBlocked()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Unblock(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<InvalidUserInputException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUnblock = DefaultId;

            var result = testedApi.Unblock(idToUnblock) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void Unblock_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Unblock(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<UnauthorizedAccessException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idTounblock = DefaultId;

            var result = testedApi.Unblock(idTounblock) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DeleteUser_ShouldDelete_WhenInputIsValid()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToUnblock = DefaultId;

            var result = testedApi.DeleteUser(idToUnblock) as ObjectResult;
            var expectedCode = StatusCodes.Status200OK;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DeleteUser_ShouldThrow_WhenUserNotFound()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Delete(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<EntityNotFoundException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToDelete = DefaultId;

            var result = testedApi.DeleteUser(idToDelete) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void DeleteUser_ShouldThrow_WhenLoggedUserIsNotAdmin()
        {
            var mockUserServices = GetTestUserServices();
            mockUserServices.Setup(services => services.Delete(It.IsAny<int>(), It.IsAny<int>()))
            .Throws<UnauthorizedAccessException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new UsersApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            int idToDelete = DefaultId;

            var result = testedApi.DeleteUser(idToDelete) as ObjectResult;
            var expectedCode = StatusCodes.Status401Unauthorized;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }
    }
}
