using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;

using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

        [HttpGet("")]
        public IActionResult FilterUsers([FromHeader] string login, [FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                List<UserResponseDto> filterResult = this.userServices.FilterBy(login, filterParameters);
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
        [HttpPost("/security/createToken")]
        public IActionResult CreateToken([FromHeader] string login)
        {
            try
            {
                var tokenString = this.securityServices.CreateToken(login);

                return this.StatusCode(StatusCodes.Status201Created, tokenString);
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
        

        [HttpPut("update/{id}")]
        public IActionResult UpdateUser([FromHeader] string login, int id, [FromQuery] UserUpdateDto updateData)
        {
            try
            {
                User updatedUser = this.userServices.Update(login, id, updateData);

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

        
        [HttpPut("promote/{id}")]
        public IActionResult PromoteToAdmin([FromHeader] string login, int id)
        {
            try
            {
                User updatedUser = this.userServices.PromoteToAdmin(login, id);

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

        [HttpPut("demote/{id}")]
        public IActionResult DemoteFromAdmin([FromHeader] string login, int id)
        {
            try
            {
                User updatedUser = this.userServices.DemoteFromAdmin(login, id);

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
                
        [HttpPut("block/{id}")]
        public IActionResult Block([FromHeader] string login, int id)
        {
            try
            {
                User updatedUser = this.userServices.Block(login, id);

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

        
        [HttpPut("unblock/{id}")]
        public IActionResult Unblock([FromHeader] string login, int id)
        {
            try
            {
                User updatedUser = this.userServices.Unblock(login, id);

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

        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromHeader] string login, int id)
        {
            try
            {
                var deletedUser = this.userServices.Delete(login, id);

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
