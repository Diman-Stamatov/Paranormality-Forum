using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            var userProfile = UserServices.GetById(id);
            return View(userProfile);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create()
        {            
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }

        
    }
}
