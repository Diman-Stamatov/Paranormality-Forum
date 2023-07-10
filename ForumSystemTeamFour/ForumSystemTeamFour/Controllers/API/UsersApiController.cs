using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
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
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ForumSystemTeamFour.Controllers.API
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly ISecurityServices securityServices;

        public UsersApiController(IUserServices userServices, ISecurityServices securityServices)
        {
            this.userServices = userServices;
            this.securityServices = securityServices;
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult FilterUsers([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                List<UserResponseDto> filterResult = userServices.FilterBy(loggedUserId, filterParameters);
                return StatusCode(StatusCodes.Status200OK, filterResult);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }           
        }

        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult CreateUser([FromBody] UserCreateDto userDto)
        {
            try
            {
                var createdUser = userServices.Create(userDto);

                return StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (DuplicateEntityException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("/security/login")]
        public IActionResult Login([FromQuery] string username, string password)
        {
            try
            {
                var token = securityServices.CreateApiToken(username, password);
                Response.Cookies.Append("Cookie_JWT", token);
                return StatusCode(StatusCodes.Status201Created, token);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            
        }

        [Authorize]
        [HttpPost("/security/logout")]
        public IActionResult DeleteToken()
        {
            Response.Cookies.Delete("Cookie_JWT");
            return StatusCode(StatusCodes.Status200OK, "You have successfully logged out!");
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(int id, [FromQuery] UserUpdateDto updateData)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                var updatedUser = userServices.Update(loggedUserId, id, updateData);

                return StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (DuplicateEntityException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }            
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("promote/{id}")]
        public IActionResult PromoteToAdmin([Required] int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                var updatedUser = userServices.PromoteToAdmin(loggedUserId, id);

                return StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }            
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("demote/{id}")]
        public IActionResult DemoteFromAdmin(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();
                var updatedUser = userServices.DemoteFromAdmin(loggedUserId, id);

                return StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }            
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("block/{id}")]
        public IActionResult Block(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                var updatedUser = userServices.Block(loggedUserId, id);

                return StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }            
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("unblock/{id}")]
        public IActionResult Unblock(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                var updatedUser = userServices.Unblock(loggedUserId, id);

                return StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }            
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                int loggedUserId = LoggedUserIdFromClaim();

                var deletedUser = userServices.Delete(loggedUserId, id);

                return StatusCode(StatusCodes.Status200OK, deletedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }           
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        private int LoggedUserIdFromClaim()
        {            
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value;

            return int.Parse(userIdClaim);
        }

    }
}
