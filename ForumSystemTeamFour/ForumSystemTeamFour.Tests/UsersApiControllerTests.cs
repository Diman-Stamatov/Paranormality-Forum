using ForumSystemTeamFour.Services;
using static ForumSystemTeamFour.Tests.TestData.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystemTeamFour.Controllers;
using ForumSystemTeamFour.Models.QueryParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using ForumSystemTeamFour.Models.DTOs;

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
   
    }
}
