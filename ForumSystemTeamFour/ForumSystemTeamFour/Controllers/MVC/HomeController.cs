using ForumSystemTeamFour.Exceptions;
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
        private readonly IHttpContextAccessor ContextAccessor;
        public HomeController(IThreadService threadService, IHttpContextAccessor contextAccessor)
        {
            this.ThreadService = threadService;
            this.ContextAccessor = contextAccessor;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                if (!HttpContext.Session.Keys.Contains("loggedUser"))
                {

                    var threads = this.ThreadService.GetAll().OrderBy(thread => thread.Replies.Count).ToList();
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
        


    }
}
