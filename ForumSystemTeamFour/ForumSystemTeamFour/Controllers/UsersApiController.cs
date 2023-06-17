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

namespace ForumSystemTeamFour.Controllers
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
        public IActionResult FilterUsers([FromHeader][Required] int loggedUserId, [FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                List<UserResponseDto> filterResult = this.userServices.FilterBy(loggedUserId, filterParameters);
                return this.StatusCode(StatusCodes.Status200OK, filterResult);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult CreateUser([FromBody] UserCreateDto userDto)
        {
            try
            {
                var createdUser = this.userServices.Create(userDto);

                return this.StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (DuplicateEntityException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("/security/login")]
        public IActionResult CreateToken([FromHeader] string login)
        {
            try
            {
                var token = this.securityServices.CreateToken(login);
                Response.Cookies.Append("Cookie_JWT", token);
                return this.StatusCode(StatusCodes.Status201Created, token);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpPost("/security/logout")]
        public IActionResult DeleteToken()
        {
                Response.Cookies.Append("Cookie_JWT", "noToken");
                return this.StatusCode(StatusCodes.Status200OK, "You have successfully logged out!");
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public IActionResult UpdateUser([FromHeader][Required] int loggedUserId, int id, [FromQuery] UserUpdateDto updateData)
        {
            try
            {
                User updatedUser = this.userServices.Update(loggedUserId, id, updateData);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (DuplicateEntityException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("promote/{id}")]
        public IActionResult PromoteToAdmin([FromHeader][Required] int loggedUserId, int id)
        {
            try
            {
                User updatedUser = this.userServices.PromoteToAdmin(loggedUserId, id);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("demote/{id}")]
        public IActionResult DemoteFromAdmin([FromHeader][Required] int loggedUserId, int id)
        {
            try
            {
                User updatedUser = this.userServices.DemoteFromAdmin(loggedUserId, id);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("block/{id}")]
        public IActionResult Block([FromHeader][Required] int loggedUserId, int id)
        {
            try
            {
                User updatedUser = this.userServices.Block(loggedUserId, id);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpPut("unblock/{id}")]
        public IActionResult Unblock([FromHeader][Required] int loggedUserId, int id)
        {
            try
            {
                User updatedUser = this.userServices.Unblock(loggedUserId, id);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (EntityNotFoundException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidUserInputException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromHeader][Required] int loggedUserId, int id)
        {
            try
            {
                var deletedUser = this.userServices.Delete(loggedUserId, id);

                return this.StatusCode(StatusCodes.Status200OK, deletedUser);
            }
            catch (EntityNotFoundException exception) 
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            catch (BadHttpRequestException exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, exception.Message);
            }
        }

    }
}
