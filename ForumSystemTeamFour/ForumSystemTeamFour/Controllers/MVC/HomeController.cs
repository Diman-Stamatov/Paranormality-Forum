using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
                
                var threads = this.ThreadService.GetAll().OrderBy(thread=>thread.Replies.Count).ToList();
                return View("AnonymousHome", threads);
            }
            else
            {
                return View("UserHome");
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error404()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Append("Cookie_JWT", "noToken");
            return View("LogoutPage");
        }
    }
}
