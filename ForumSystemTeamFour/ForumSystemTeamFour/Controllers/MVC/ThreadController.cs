using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Services.Interfaces;
using ForumSystemTeamFour.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystemTeamFour.Mappers.Interfaces;
using System;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Controllers.MVC
{
    public class ThreadController : Controller
    {
        private readonly IThreadService ThreadServices;
        private readonly ISecurityServices SecurityServices;
        private readonly IThreadMapper ThreadMapper;

        public ThreadController(IThreadService threadServices,
                                ISecurityServices securityServices,
                                IThreadMapper threadMapper)
        {
            this.ThreadServices = threadServices;
            this.SecurityServices = securityServices;
            this.ThreadMapper = threadMapper;
        }

        [HttpPost]
        public IActionResult Index([FromQuery] int id, ThreadQueryParameters ThreadQueryParameters)
        {
            try
            {
                //ToDo как да проверя дали сесията е валидна?

                var threads = this.ThreadServices.FilterBy(id, ThreadQueryParameters);
                return View(threads);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
