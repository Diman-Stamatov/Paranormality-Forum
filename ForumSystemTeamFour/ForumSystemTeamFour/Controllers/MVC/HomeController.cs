using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForumSystemTeamFour.Controllers.MVC
{

    public class HomeController : Controller
    {
        private readonly IThreadService ThreadService;

        public HomeController(IThreadService threadService)
        {
            this.ThreadService = threadService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var threads = this.ThreadService.GetAllLarge()
                    .OrderByDescending(thread => thread.Replies.Count).Take(10).ToList();
                return this.View("AnonymousHome", threads);
            }
            else
            {
                return this.View("UserHome");
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error404()
        {
            return this.View("Error404");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error401()
        {
            return this.View("Error401");
        }

        
    }
}
