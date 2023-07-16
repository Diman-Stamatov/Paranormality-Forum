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
        public IActionResult Create([FromRoute] int id)
        {
            this.ViewData["ThreadId"] = id;

            var replyCreateViewModel = new ReplyCreateViewModel()
            {
                ThreadId = id
            };

            return View(replyCreateViewModel);
        }

        // POST: RepliesController/Create
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromRoute] int id, ReplyCreateViewModel replyCreateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    replyCreateViewModel.ThreadId = id;
                    return View(replyCreateViewModel);
                }

                var newReply = replyService.Create(replyCreateViewModel, GetLoggedUserId());

                return RedirectToAction("Details", "Replies", new { id = newReply.Id });
            }
			catch (UnauthorizedAccessException exception)
			{
				this.Response.StatusCode = StatusCodes.Status401Unauthorized;
				this.ViewData["ErrorMessage"] = exception.Message;

				return this.View("Error401");
			}
		}

        // GET: RepliesController/Edit/5
        [Authorize]
        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            try
            {
                int loggedUserId = GetLoggedUserId();

                var replyViewModel = replyService.GetViewModelById(id);

                if(loggedUserId == replyViewModel.AuthorId)
                {
                    return View(replyViewModel);
                }
                return RedirectToAction("Details", "Replies", new {id = id});
            }
            catch (EntityNotFoundException exception)
            {
				this.Response.StatusCode = StatusCodes.Status404NotFound;
				this.ViewData["ErrorMessage"] = exception.Message;
				return View("Error404");
            }
			catch (UnauthorizedAccessException exception)
			{
				this.Response.StatusCode = StatusCodes.Status401Unauthorized;
				this.ViewData["ErrorMessage"] = exception.Message;

				return this.View("Error401");
			}
		}

        // POST: RepliesController/Edit/5
        [Authorize]
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, ReplyViewModel replyViewModel)
        {
            try
            {

                if(!ModelState.IsValid)
                {
                    var modifiedReply = replyService.GetViewModelById(id);
                    modifiedReply.Content = replyViewModel.Content;
                    return View(modifiedReply);
                }

                var updatedViewModel = replyService.Update(id, replyViewModel, GetLoggedUserId());

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
        public IActionResult Delete([FromRoute] int id)
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
                var replyToDelete = replyService.Delete(replyViewModel.Id, GetLoggedUserId());

                return RedirectToAction("Details", "Threads", new { id = replyToDelete.ThreadId });
            }
            catch (EntityNotFoundException exception)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error404");
            }
        }
        public IActionResult Upvote(int id,  int threadId)
        {
            try
            {
                var replyToUpvote = replyService.UpVote(id, GetLoggedUserId());

				if (threadId != 0)
				{
					return RedirectToAction("Details", "Thread", new { id = threadId });
				}
				return RedirectToAction("Details", "Replies", new { id = replyToUpvote.Id });
            }
            catch (EntityNotFoundException exception)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error404");
            }
            catch (UnauthorizedAccessException exception)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error401");
            }
            catch (Exception exception)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error500");
            }
        }
        public IActionResult DownVote(int id, int threadId)
        {
            try
            {
                var replyToDownvote = replyService.DownVote(id, GetLoggedUserId());

                if (threadId!=0)
                {
					return RedirectToAction("Details", "Thread", new { id = threadId });
				}
                return RedirectToAction("Details", "Replies", new { id = replyToDownvote.Id });

            }
            catch (EntityNotFoundException exception)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error404");
            }
            catch (UnauthorizedAccessException exception)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error401");
            }
            catch (Exception exception)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View("Error500");
            }
        }
        private int GetLoggedUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
        }
    }
}
