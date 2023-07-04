using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Services.Interfaces;
using ForumSystemTeamFour.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystemTeamFour.Mappers.Interfaces;
using System;

namespace ForumSystemTeamFour.Controllers.MVC
{
    
    public class UsersController : Controller
    {
        private readonly IUserServices UserServices;
        private readonly IUserMapper UserMapper;
        public UsersController(IUserServices userServices, IUserMapper userMapper) 
        {
            this.UserServices = userServices;
            this.UserMapper = userMapper;            
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile([FromRoute] int id)
        {
            try
            {
                var specifiedUser = UserServices.GetById(id);
                return this.View(specifiedUser);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error404");
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            var userCreateVM = new UserCreateVM();
            return this.View("Create", userCreateVM);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(UserCreateVM userCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(userCreateVM);
            }
            if (userCreateVM.Password != userCreateVM.ConfirmPassword)
            {
                this.ModelState.AddModelError("Password", "The provided passwords do not match!");
                return this.View(userCreateVM);
            }

            try
            {
                var newUserDto = UserMapper.Map(userCreateVM);
                var createdUser = UserServices.Create(newUserDto);
            }
            catch (DuplicateEntityException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                
                return this.View(userCreateVM);
            }
            
            return RedirectToAction("Login", "Users");

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return this.View("Login");
        }

        private string ShortenErrorMessage(string errorMessage)
        {
            string messageStart = "must";
            string shortenedMessage = errorMessage.Substring(errorMessage.IndexOf(messageStart));
            return shortenedMessage;
        }

        
    }
}
