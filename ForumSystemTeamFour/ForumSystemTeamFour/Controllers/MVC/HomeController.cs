using ForumSystemTeamFour.Exceptions;
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
        private readonly IThreadRepositroy ThreadRepository;

        public HomeController(IThreadRepositroy threadRepository)
        {
            this.ThreadRepository = threadRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {

                    var threads = this.ThreadRepository.GetAll().OrderByDescending(thread => thread.Replies.Count).Take(10).ToList();
                    return this.View("AnonymousHome", threads);
                }
                else
                {
                    
                    return this.View("UserHome");
                }
            }
            catch (EntityNotFoundException)
            {

                return this.View("Error404");
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
