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
        public IActionResult Create()
        {
            return View();
        }

        // POST: RepliesController/Create
        [Authorize]
        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction("Details", "Replies", id);
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
                    var originalReply = replyService.GetViewModelById(id);
                    originalReply.Content = replyViewModel.Content;
                    return View(originalReply);
                }
                
                var loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);

                var updatedViewModel = replyService.Update(id, replyViewModel, loggedUserId);

                return RedirectToAction("Details", "Replies", new { id = updatedViewModel.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RepliesController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: RepliesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
