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
        private readonly ISecurityServices SecurityServices;
        private readonly IUserMapper UserMapper;
        public UsersController(IUserServices userServices, IUserMapper userMapper, ISecurityServices securityServices) 
        {
            this.UserServices = userServices;
            this.UserMapper = userMapper; 
            this.SecurityServices = securityServices;
        }

        [AllowAnonymous]
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
        public IActionResult Register()
        {
            var userCreateVM = new UserCreateVM();
            return this.View("Register", userCreateVM);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(UserCreateVM userCreateVM)
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
                this.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return this.View(userCreateVM);
            }
            
            return RedirectToAction("Login", "Users");

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            var loginVM = new LoginVM();
            return this.View("Login", loginVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return this.View(loginVM);
            }            

            try
            {
                string loginUsername = loginVM.Username;
                string loginPassword = loginVM.Password;
                
                var loggedUser = SecurityServices.Authenticate(loginUsername, loginPassword);
                if (loggedUser.IsAdmin)
                {
                    SecurityServices.CreateApiToken(loginUsername, loginPassword);
                }
                this.HttpContext.Session.SetString("loggedUser",loggedUser.Username);
                this.HttpContext.Session.SetString("isAdmin", loggedUser.IsAdmin.ToString());
                this.HttpContext.Session.SetString("userId", loggedUser.Id.ToString());

                return RedirectToAction("Index", "Home");
            }
            catch (EntityNotFoundException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                return this.View(loginVM);
            }
            catch (InvalidUserInputException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                this.ViewData["ErrorMessage"] = exception.Message;

                return this.View(loginVM);
            }

            

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");
            Response.Cookies.Append("Cookie_JWT", "noToken");
            return this.View("LogoutPage");
        }



    }
}
