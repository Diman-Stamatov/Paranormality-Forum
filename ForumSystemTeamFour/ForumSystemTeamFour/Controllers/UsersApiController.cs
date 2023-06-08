using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            this.userServices = userServices;
        }

        [HttpGet("")]
        public IActionResult GetUsers([FromQuery] UserQueryParameters filterParameters)
        {
            List<User> result = this.userServices.FilterBy(filterParameters);

            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = this.userServices.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (InvalidOperationException exception) //ToDo Custom Exceptions
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var user = this.userMapper.Map(userDto); //ToDo Throw Duplicate Username exception in usernameRepository.Create?
                var createdUser = this.userServices.Create(user);

                return this.StatusCode(StatusCodes.Status201Created, createdUser); //ToDo Custom Exceptions
            }
            catch (InvalidOperationException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            try
            {
                User user = this.userMapper.Map(userDto);
                User updatedUser = this.userServices.Update(id, user);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (InvalidOperationException exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message); //ToDo Custom Exceptions
            }
            /*catch (InvalidOperationException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message); //ToDo Custom Exceptions
            }*/
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var deletedUser = this.userServices.Delete(id);

                return this.StatusCode(StatusCodes.Status200OK, deletedUser);
            }
            catch (InvalidOperationException exception) //ToDo Custom Exceptions
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
    }
}
