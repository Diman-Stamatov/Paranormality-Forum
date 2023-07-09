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
    public class ThreadApiControllerTests
    {
        private void SetControllerContext(ThreadApiController controller)
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("LoggedUserId", DefaultId.ToString()));
            var principal = new ClaimsPrincipal(identity);
            var context = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

            controller.ControllerContext = context;
        }

        [TestMethod]
        public void CreateThread_ShouldCreate_WhenInputIsValid()
        {
            var mockUserServices = GetTestThreadServices().Object;
            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new ThreadApiController(mockUserServices, mockSecurityServices);
            SetControllerContext(testedApi);

            var testCreateDto = GetTestThreadCreateDto();

            var result = testedApi.Create(testCreateDto) as ObjectResult;
            var expectedCode = StatusCodes.Status201Created;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        [TestMethod]
        public void CreateThread_ShouldThrow_WhenInputIsDuplicate()
        {
            var mockUserServices = GetTestThreadServices();
            mockUserServices.Setup(services => services.Create(It.IsAny<ThreadCreateDto>(), It.IsAny<int>()))
            .Throws<DuplicateEntityException>();

            var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

            var testedApi = new ThreadApiController(mockUserServices.Object, mockSecurityServices);
            SetControllerContext(testedApi);

            var testCreateDto = GetTestThreadCreateDto();

            var result = testedApi.Create(testCreateDto) as ObjectResult;
            var expectedCode = StatusCodes.Status409Conflict;
            Assert.AreEqual(expectedCode, result.StatusCode);
        }

        //[TestMethod]
        //public void CreateToken_ShouldCreate_WhenInputIsValid()
        //{
        //    var mockUserServices = GetTestThreadServices().Object;
        //    var mockSecurityServices = GetValidAuthenticationTestSecurity().Object;

        //    var testedApi = new ThreadApiController(mockUserServices, mockSecurityServices);
        //    SetControllerContext(testedApi);

        //    string testUsername = "testLogin";
        //    string testPassword = "testPassword";

        //    var result = testedApi.Login(testUsername, testPassword) as ObjectResult;
        //    var expectedCode = StatusCodes.Status201Created;
        //    Assert.AreEqual(expectedCode, result.StatusCode);
        //}
    }
}
