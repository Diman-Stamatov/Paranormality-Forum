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
    internal class ThreadApiControllerTests
    {
    }
}
