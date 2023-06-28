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

            var testCreateDto = GetTestUserCreateDto();
            string testLogin = "testLogin";

            var result = testedApi.CreateToken(testLogin) as ObjectResult;
            var expectedCode = StatusCodes.Status201Created;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateToken_ShouldThrow_WhenLoginIsWrong()
        {
            var mockUserServices = GetTestUserServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity();
            mockSecurityServices.Setup(services => services.CreateToken(It.IsAny<string>()))
            .Throws<EntityNotFoundException>();

            var testedApi = new UsersApiController(mockUserServices, mockSecurityServices.Object);
            SetControllerContext(testedApi);

            var testCreateDto = GetTestUserCreateDto();
            string testLogin = "testLogin";

            var result = testedApi.CreateToken(testLogin) as ObjectResult;
            var expectedCode = StatusCodes.Status404NotFound;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }
    }
}
