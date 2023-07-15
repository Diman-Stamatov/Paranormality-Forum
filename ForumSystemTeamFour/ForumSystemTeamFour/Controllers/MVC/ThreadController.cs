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

                return this.View(threads);
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

        [Authorize]
        [HttpGet]
        public IActionResult FilterBy()
        {
            var threadQueryParameters = new ThreadQueryParameters();
            return this.View("Index", threadQueryParameters);
        }

        [Authorize]
        [HttpPost]
        public IActionResult FilterBy(ThreadQueryParameters threadQueryParameters)
        {
            var loggedUser = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
            string loggedAdmin = User.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin").Value;

            try
            {
                if (threadQueryParameters.Email != null && loggedAdmin != "True")
                {
                    throw new UnauthorizedAccessException("You are not authorized to search by email.");
                }
                var listOfResponseDtos = this.ThreadServices.FilterBy(loggedUser, threadQueryParameters);
                return this.View("Index", listOfResponseDtos);
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
        public IActionResult Delete([FromRoute] int id, LargeThreadResponseDto threadUpdateDto)
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Details([FromRoute]int id, ThreadDetailsVM detailsVM)
        {
            var queryParameters = ReplyMapper.MapViewQuery(detailsVM.QueryParameters);
            
            try
            {
				detailsVM.Thread = ThreadServices.Details(id);
				var sortedReplies = ReplyServices.FilterBy(queryParameters);
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
        private void InitializeDropDownListsOfTags(ThreadCreateVM threadViewModel)
        {
            threadViewModel.Tags = new SelectList(ThreadServices.GetAll(), "Id", "Name");
        }
    }
}
