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
using System.Linq;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;

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
            var threadCreateDto = new ThreadCreateDto();
            try
            {
                threadCreateDto.AuthorId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
            }
            catch (UnauthorizedAccessException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;
                return RedirectToAction("Login", "Users");
            }
            threadCreateDto.AuthorId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
            return this.View("Create", threadCreateDto);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ThreadCreateDto threadCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return this.View(threadCreateDto);
            }
            try
            {
                int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
                var createdThread = this.ThreadServices.Create(threadCreateDto, loggedUserId);
                return RedirectToAction("Details", new { id = createdThread.Id }); ;
            }
            catch (DuplicateEntityException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return this.View(threadCreateDto);
            }
            catch (UnauthorizedAccessException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;
                return RedirectToAction("Login", "Users");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var thread = this.ThreadServices.Details(id);
                var threadUpdateDto = new ThreadUpdateDto();
                string loggedUsername = User.Claims.FirstOrDefault(claim => claim.Type == "Username").Value;
                if (loggedUsername != thread.Author.Username && !User.IsInRole("Admin"))
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
        public IActionResult Update(int id, ThreadUpdateDto threadUpdateDto)
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
            catch (UnauthorizedAccessException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error401");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                string loggedUsername = User.Claims.FirstOrDefault(claim => claim.Type == "Username").Value;
                var threadToDelete = this.ThreadServices.Details(id);
                if (loggedUsername != threadToDelete.Author.Username && !User.IsInRole("Admin"))
                {
                    throw new UnauthorizedAccessException("You are not authorized to delete this thread.");
                }
                if (threadToDelete.isDeleted)
                {
                    throw new EntityNotFoundException("Post can not be found.");
                }
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
        public IActionResult Delete(int id, LargeThreadResponseDto threadUpdateDto)
        {
            try
            {
                int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
                var deletedThread = this.ThreadServices.Delete(id, loggedUserId);
                if (loggedUserId != deletedThread.AuthorId && !User.IsInRole("Admin"))
                {
                    throw new UnauthorizedAccessException("You are not authorized to delete this thread.");
                }
                if (deletedThread.IsDeleted)
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
