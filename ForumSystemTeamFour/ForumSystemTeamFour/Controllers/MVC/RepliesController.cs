using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ForumSystemTeamFour.Controllers.MVC
{
    public class RepliesController : Controller
    {
        private readonly IReplyService replyService;

        public RepliesController(IReplyService replyService)
        {
            this.replyService = replyService;
        }

        // GET: RepliesController
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            if (ViewData["ThreadId"] != null)
            {
                return RedirectToAction("Index", "Thread", ViewData["ThreadId"]);
            }
            return RedirectToAction("Index", "Thread");
        }

        // GET: RepliesController/Details/5
        [Authorize]
        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                var replyViewModel = replyService.GetViewModelById(id);
                return View(replyViewModel);
            }
            catch (EntityNotFoundException)
            {
                return View("Error404");
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: RepliesController/Create
        [Authorize]
        [HttpGet]
        public IActionResult Create([FromQuery] int threadId)
        {
            this.ViewData["ThreadId"] = threadId;

            var replyCreateViewModel = new ReplyCreateViewModel()
            {
                ThreadId = threadId
            };

            return View(replyCreateViewModel);
        }

        // POST: RepliesController/Create
        [Authorize]
        [HttpPost]
        public IActionResult Create(ReplyCreateViewModel replyCreateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(replyCreateViewModel);
                }

                var loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);

                var newReply = replyService.Create(replyCreateViewModel, loggedUserId);

                return RedirectToAction("Details", "Replies", new { id = newReply.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RepliesController/Edit/5
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);

                var replyViewModel = replyService.GetViewModelById(id);

                if(loggedUserId == replyViewModel.AuthorId)
                {
                    return View(replyViewModel);
                }
                return RedirectToAction("Details", "Replies", new {id = id});
            }
            catch (EntityNotFoundException)
            {
                return View("Error404");
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: RepliesController/Edit/5
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ReplyViewModel replyViewModel)
        {
            try
            {

                if(!ModelState.IsValid)
                {
                    var modifiedReply = replyService.GetViewModelById(id);
                    modifiedReply.Content = replyViewModel.Content;
                    return View(modifiedReply);
                }
                
                var loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);

                var updatedViewModel = replyService.Update(id, replyViewModel, loggedUserId);

                return RedirectToAction("Details", "Replies", new { id = updatedViewModel.Id });
            }
            catch (EntityNotFoundException exception)
            {
                ViewData["ErrorMessage"] = exception.Message;
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return View(replyViewModel);
            }
        }

        // GET: RepliesController/Delete/5
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var replyViewModel = replyService.GetViewModelById(id);
                return View(replyViewModel);
            }
            catch (EntityNotFoundException)
            {
                return View("Error404");
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: RepliesController/Delete/5
        [Authorize]
        [HttpPost]
        public IActionResult Delete(ReplyViewModel replyViewModel)
        {
            try
            {
                int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
                var replyToDelete = replyService.Delete(replyViewModel.Id, loggedUserId);

                return RedirectToAction("Details", "Threads", new { id = replyToDelete.ThreadId });
            }
            catch (EntityNotFoundException exception)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error404");
            }
        }
    }
}
