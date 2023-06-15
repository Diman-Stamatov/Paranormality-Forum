using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Security;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserServices userServices;

        public UsersApiController(IUserServices userServices)
        {
            this.userServices = userServices;
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

        [Route("promote/{username}")]
        [HttpPut("{username}")]
        public IActionResult PromoteToAdmin([FromHeader] string login, string username)
        {
            try
            {
                User updatedUser = this.userServices.PromoteToAdmin(login, username);

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

        [Route("demote/{username}")]
        [HttpPut("{username}")]
        public IActionResult DemoteFromAdmin([FromHeader] string login, string username)
        {
            try
            {
                User updatedUser = this.userServices.DemoteFromAdmin(login, username);

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

        [Route("block/{username}")]
        [HttpPut("{username}")]
        public IActionResult Block([FromHeader] string login, string username)
        {
            try
            {
                User updatedUser = this.userServices.Block(login, username);

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

        [Route("unblock/{username}")]
        [HttpPut("{username}")]
        public IActionResult Unblock([FromHeader] string login, string username)
        {
            try
            {
                User updatedUser = this.userServices.Unblock(login, username);

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

        [HttpDelete("{username}")]
        public IActionResult DeleteUser([FromHeader] string login, string username)
        {
            try
            {
                var deletedUser = this.userServices.Delete(login, username);

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
