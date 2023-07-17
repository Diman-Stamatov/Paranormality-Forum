using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystemTeamFour.Mappers.Interfaces;
using System;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Models.QueryParameters;
using System.Linq;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using System.Data;
using ForumSystemTeamFour.Models.ViewModels.User;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models.ViewModels.Thread;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumSystemTeamFour.Controllers.MVC
{
    public class ThreadController : Controller
    {
        private readonly IThreadService ThreadServices;
        private readonly ISecurityServices SecurityServices;
        private readonly IThreadMapper ThreadMapper;
        private readonly IReplyService ReplyServices;
        private readonly IReplyMapper ReplyMapper;

        public ThreadController(IThreadService threadServices,
                                ISecurityServices securityServices,
                                IThreadMapper threadMapper,
                                IReplyService replyServices,
                                IReplyMapper replyMapper)
        {
            this.ThreadServices = threadServices;
            this.SecurityServices = securityServices;
            this.ThreadMapper = threadMapper;
            this.ReplyServices = replyServices;
            this.ReplyMapper = replyMapper;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var threads = this.ThreadServices.GetAll();

                if (threads == null || !threads.Any())
                {
                    throw new EntityNotFoundException("No threads found. Be the first to create a thread!");
                }

                var threadIndexVM = this.ThreadMapper.MapVM(threads);
                return this.View(threadIndexVM);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.RedirectToAction("Create", "Threads");
            }
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var threadCreateDto = new ThreadCreateVM();
            this.InitializeDropDownListsOfTags(threadCreateDto);
            threadCreateDto.AuthorId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
            return this.View("Create", threadCreateDto);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(ThreadCreateVM threadCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return this.View(threadCreateDto);
            }
            int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
            var createdThread = this.ThreadServices.Create(threadCreateDto, loggedUserId);
            return RedirectToAction("Details", new { id = createdThread.Id });

        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            var detailsVM = new ThreadDetailsVM();
            try
            {
                detailsVM.Thread = ThreadServices.Details(id);
            }
            catch (EntityNotFoundException exception)
            {

                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return this.View("Error404");
            }
            return this.View("Details", detailsVM);
        }

        [Authorize]
        [HttpGet]
        public IActionResult FilterBy(string tag)
        {
            var threadIndexVM = new ThreadIndexVM();
            return this.View("Index", threadIndexVM);
        }
        [Authorize]
        [HttpPost]
        public IActionResult FilterBy(ThreadIndexVM threadIndexVM)
        {
            var loggedUser = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
            string loggedAdmin = User.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin").Value;

            try
            {
                if (loggedAdmin != "True" && threadIndexVM.QueryParameters.Email != null)
                {
                    throw new UnauthorizedAccessException("You are not authorized to search by email.");
                }
                var listOfResponseDtos = this.ThreadServices.FilterForVM(threadIndexVM.QueryParameters);
                threadIndexVM.Threads = listOfResponseDtos;
                return this.View("Index", threadIndexVM);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error404");
            }
            catch (UnauthorizedAccessException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Index");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Details([FromRoute] int id, ThreadDetailsVM detailsVM)
        {
            var queryParameters = ReplyMapper.MapViewQuery(detailsVM.QueryParameters);

            try
            {
                detailsVM.Thread = ThreadServices.Details(id);
                var sortedReplies = ReplyServices.FilterForVM(queryParameters);
                detailsVM.Thread.Replies = sortedReplies;
            }
            catch (EntityNotFoundException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message + " Displaying the original replies.";
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return this.View(detailsVM);
            }
            return this.View("Details", detailsVM);
        }
        private int GetLoggedUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
        }
        private void InitializeDropDownListsOfTags(ThreadCreateVM threadViewModel)
        {
            var allTags = ThreadServices.GetAllTags();

            threadViewModel.TagsList = new SelectList(allTags, "Id", "Name");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Update([FromRoute] int id)
        {
            try
            {
                var thread = this.ThreadServices.Details(id);
                var threadUpdateDto = new ThreadUpdateDto();
                string loggedUsername = User.Claims.FirstOrDefault(claim => claim.Type == "Username").Value;
                string loggedAdmin = User.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin").Value;
                if (loggedUsername != thread.Author.Username && loggedAdmin != "True")
                {
                    throw new UnauthorizedAccessException("You are not authorized to update this thread.");
                }
                return this.View("Update", threadUpdateDto);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error404");
            }
            catch (UnauthorizedAccessException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error401");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update([FromRoute] int id, ThreadUpdateDto threadUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return this.View(threadUpdateDto);
            }
            try
            {
                int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
                var updatedThread = this.ThreadServices.Update(id, threadUpdateDto, loggedUserId);
                return RedirectToAction("Details", new { id = updatedThread.Id });
            }
            catch (EntityNotFoundException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return this.View(threadUpdateDto);
            }
        }

        [Authorize]
        public IActionResult Upvote(int id, int threadId)
        {
            try
            {
                var threadToUpvote = ThreadServices.UpVote(id, GetLoggedUserId());

                return RedirectToAction("Details", "Thread", new { id = threadToUpvote.Id });
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
                var threadToDownvote = ThreadServices.DownVote(id, GetLoggedUserId());

                //if (threadId != 0)
                //{
                //	return RedirectToAction("Details", "Thread", new { id = threadId });
                //}
                return RedirectToAction("Details", "Thread", new { id = threadToDownvote.Id });

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
        [Authorize]
        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {

                var threadToDelete = this.ThreadServices.Details(id);
                string loggedAdmin = User.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin").Value;
                if (loggedAdmin != "True")
                {
                    throw new UnauthorizedAccessException("You are not authorized to delete this thread.");
                }
                if (threadToDelete.isDeleted)
                {
                    throw new EntityNotFoundException("Post can not be found.");
                }
                //ToDo може да е view, което да пита дали искаш да изтриеш този Thread
                return this.View("Delete", threadToDelete);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error404");
            }
            catch (UnauthorizedAccessException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error401");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete([FromRoute] int id, ThreadVM threadUpdateDto)
        {
            try
            {
                string loggedAdmin = User.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin").Value;
                if (loggedAdmin != "True")
                {
                    throw new UnauthorizedAccessException("You are not authorized to delete this thread.");
                }
                if (threadUpdateDto.isDeleted)
                {
                    throw new EntityNotFoundException("Post can not be found.");
                }
                return RedirectToAction("Index", "Threads");
            }
            catch (EntityNotFoundException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return this.View(threadUpdateDto);
            }
            catch (UnauthorizedAccessException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return this.View(threadUpdateDto);
            }
        }

    }
}
