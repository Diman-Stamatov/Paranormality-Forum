using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;

using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ForumSystemTeamFour.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    public class ThreadApiController : ControllerBase
    {
        private readonly IThreadService threadService;
        private readonly ISecurityServices securityServices;

        public ThreadApiController(IThreadService threadService,
                                    ISecurityServices securityServices)
        {
            this.threadService = threadService;
            this.securityServices = securityServices;
        }

    }
}
