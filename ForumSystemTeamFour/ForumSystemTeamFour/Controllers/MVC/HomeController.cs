using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForumSystemTeamFour.Controllers.MVC
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IThreadService ThreadService;
        public HomeController(IThreadService threadService)
        {
            this.ThreadService = threadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var threads = this.ThreadService.GetAll().OrderBy(thread=>thread.Comments.Count).ToList();
                return View("AnonymousHome", threads);
            }
            else
            {
                return View("UserHome");
            }
            
        }

        [HttpGet]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
