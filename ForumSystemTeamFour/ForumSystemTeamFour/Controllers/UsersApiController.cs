using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly UserMapper userMapper;

        public UsersApiController(IUserServices userServices, UserMapper userMapper)
        {
            this.userServices = userServices;
            this.userMapper = userMapper;
        }

        [HttpGet("")]
        public IActionResult GetUsers([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                List<User> result = this.userServices.FilterBy(filterParameters);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException exception)
            {

                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
            
        }       

        [HttpPost("")]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var user = this.userMapper.Map(userDto); 
                var createdUser = this.userServices.Create(user);

                return this.StatusCode(StatusCodes.Status201Created, createdUser); 
            }
            catch (DuplicateEntityException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
        }
        [Route("update/{username}")]
        [HttpPut("{username}")]
        public IActionResult UpdateUser(string username, [FromQuery] UserUpdateData updateData)
        {
            try
            {
                User updatedUser = this.userServices.Update(username, updateData);

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
        }

        [Route("promote/{username}")]
        [HttpPut("{username}")]
        public IActionResult PromoteToAdmin(string username)
        {
            try
            {
                User updatedUser = this.userServices.PromoteToAdmin(username);

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
        }

        [Route("demote/{username}")]
        [HttpPut("{username}")]
        public IActionResult DemoteFromAdmin(string username)
        {
            try
            {
                User updatedUser = this.userServices.DemoteFromAdmin(username);

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
        }

        [Route("block/{username}")]
        [HttpPut("{username}")]
        public IActionResult Block(string username)
        {
            try
            {
                User updatedUser = this.userServices.Block(username);

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
        }

        [Route("unblock/{username}")]
        [HttpPut("{username}")]
        public IActionResult Unblock(string username)
        {
            try
            {
                User updatedUser = this.userServices.Unblock(username);

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
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            //ToDo Validation
            try
            {
                var deletedUser = this.userServices.Delete(username);

                return this.StatusCode(StatusCodes.Status200OK, deletedUser);
            }
            catch (EntityNotFoundException exception) 
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
    }
}
