using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ForumSystemTeamFour.Controllers.MVC
{

    public class HomeController : Controller
    {
        private readonly IUserServices UserServices;
        private readonly IThreadService ThreadServices;
        private readonly IReplyService ReplyServices;
        private readonly ITagServices TagServices;
        private readonly ITagMapper TagMapper;
        private readonly IUserMapper UserMapper;

        public HomeController(IUserServices userServices, IThreadService threadServices,
            IReplyService replyServices, ITagServices tagServices, ITagMapper tagMapper, IUserMapper userMapper)
        {
            this.UserServices = userServices;
            this.ThreadServices = threadServices;
            this.ReplyServices = replyServices;
            this.TagServices = tagServices;
            this.TagMapper = tagMapper;
            this.UserMapper = userMapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var homeVM = new AnonymousHomeVM();
                var allTags = this.TagServices.GetAll();
                var topTags = allTags.OrderByDescending(tag => tag.Threads.Count).Take(5).ToList();
                homeVM.TopTags = this.TagMapper.Map(topTags);

                var topThreads = this.ThreadServices.GetAllVM()
                    .OrderByDescending(thread => thread.Replies.Count).Take(10).ToList();
                homeVM.TopThreads = topThreads;

                int defaultId = 1;
                var queryParameters = new UserQueryParameters();
                var allUsers = UserServices.FilterBy(defaultId, queryParameters);
                int totalUsers = UserServices.GetCount();
                var random = new Random();
                var randomUsers = allUsers.OrderBy(user => random.Next()).Take(random.Next(10, totalUsers)).ToList();
                var usernames = UserMapper.MapUsernameList(randomUsers);
                homeVM.UsersOnline = usernames.Distinct().ToList();
                homeVM.TotalUsers = totalUsers;
                homeVM.NumberOfThreads = ThreadServices.GetCount();
                homeVM.NumberOfPosts = ReplyServices.GetCount();

                return this.View("AnonymousHome", homeVM);
            }
            else
            {
				return RedirectToAction("Index", "Thread");
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult About()
        {
            var statsVM = new AnonymousHomeVM();
            var allTags = this.TagServices.GetAll();
            var topTags = allTags.OrderByDescending(tag => tag.Threads.Count).Take(5).ToList();
            statsVM.TopTags = this.TagMapper.Map(topTags);

            var topThreads = this.ThreadServices.GetAllVM()
                .OrderByDescending(thread => thread.Replies.Count).Take(10).ToList();
            statsVM.TopThreads = topThreads;

            int defaultId = 1;
            var queryParameters = new UserQueryParameters();
            var allUsers = UserServices.FilterBy(defaultId, queryParameters);
            int totalUsers = UserServices.GetCount();
            var random = new Random();
            var randomUsers = allUsers.OrderBy(user => random.Next()).Take(random.Next(10, totalUsers)).ToList();
            var usernames = UserMapper.MapUsernameList(randomUsers);
            statsVM.UsersOnline = usernames.Distinct().ToList();

            statsVM.TotalUsers = totalUsers;
            statsVM.NumberOfThreads = ThreadServices.GetCount();
            statsVM.NumberOfPosts = ReplyServices.GetCount();

            return this.View("About", statsVM);
        }

    }
}
