using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystemTeamFour.Controllers.MVC
{
    
    public class UsersController : Controller
    {
        private readonly IUserServices UserServices;
        public UsersController(IUserServices userServices) 
        {
            this.UserServices = userServices;        
        }

        
        [HttpGet]
        public IActionResult Profile([FromRoute] int id)
        {
            try
            {
                var specifiedUser = UserServices.GetById(id);
                return this.View(specifiedUser);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error404");
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {            
            return this.View("Create");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return this.View("Login");
        }

        
    }
}
