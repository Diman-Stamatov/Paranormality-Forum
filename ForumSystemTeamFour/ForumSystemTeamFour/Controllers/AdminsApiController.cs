using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminsApiController : ControllerBase
    {
        private readonly IAdminServices adminServices;
        private readonly IUserServices userServices;
        public AdminsApiController(IAdminServices adminServices, IUserServices userServices)
        {
            this.adminServices = adminServices;
            this.userServices = userServices;
        }

        [HttpGet("")]
        public IActionResult GetUsers([FromQuery] UserQueryParameters filterParameters)
        {
            List<Admin> result = this.adminServices.GetAll();

            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            try
            {
                var foundAdmin = this.adminServices.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, foundAdmin);
            }
            catch (InvalidOperationException exception) //ToDo Custom Exceptions
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
        [HttpGet("{username}")]
        public IActionResult GetAdminByUsername(string username)
        {
            try
            {
                var foundAdmin = this.adminServices.GetByUsername(username);

                return this.StatusCode(StatusCodes.Status200OK, foundAdmin);
            }
            catch (InvalidOperationException exception) //ToDo Custom Exceptions
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }

        [HttpPost("{id}")]
        public IActionResult PromoteToAdmin(int id)
        {
            try
            {
                var userToPromote = this.userServices.GetById(id);
                if (userToPromote == null)
                {
                    throw new InvalidOperationException($"A user with the ID {id} does not exist!");
                }
                var newAdmin = this.adminServices.Create(userToPromote);

                return this.StatusCode(StatusCodes.Status201Created, newAdmin); //ToDo Custom Exceptions
            }
            catch (InvalidOperationException exception)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, exception.Message);
            }
        }        

        [HttpDelete("{id}")]
        public IActionResult DemoteAdminToUser(int id)
        {
            try
            {
                var demotedAdmin = this.adminServices.Demote(id);

                return this.StatusCode(StatusCodes.Status200OK, demotedAdmin);
            }
            catch (InvalidOperationException exception) //ToDo Custom Exceptions
            {
                return this.StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
    }
}
