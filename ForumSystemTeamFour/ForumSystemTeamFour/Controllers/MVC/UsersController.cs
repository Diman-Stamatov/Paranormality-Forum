using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumSystemTeamFour.Mappers.Interfaces;
using System;
using ForumSystemTeamFour.Services;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.IO.Pipes;
using ForumSystemTeamFour.Models.ViewModels.User;

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

        [Authorize]
        [HttpGet]
        public IActionResult Profile([FromRoute] string id)
        {
            try
            {
                var profileOwnerVM = UserServices.GetUserProfileVM(id);               
                
                return this.View(profileOwnerVM);
            }
            catch (EntityNotFoundException exception)
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = exception.Message;
                return this.View("Error404");
            }            
        }
        

        [Authorize]
        [HttpGet]
        public IActionResult Update([FromRoute] string id)
        {
            
            try
            {
                var userUpdateVM = UserServices.GetUserUpdateVM(id);
                return this.View("Update", userUpdateVM);
            }
            catch (EntityNotFoundException exception)
            {

                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return this.View("Error404");
            }
            

        }

        [Authorize]
        [HttpPost]
        public IActionResult Update([FromRoute] string id, UserUpdateVM userUpdateVM)
        {
            if (!ModelState.IsValid)
            {
                
                return this.View(userUpdateVM);
            }
            if (userUpdateVM.Password != userUpdateVM.ConfirmPassword)
            {
                this.ModelState.AddModelError("Password", "The provided passwords do not match!");
                return this.View(userUpdateVM);
            }

            try
            {
                int loggedUserId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "LoggedUserId").Value);
                UserServices.Update(loggedUserId, userUpdateVM);
            }
            catch (DuplicateEntityException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return this.View(userUpdateVM);
            }
            catch (EntityNotFoundException exception)
            {
                
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return this.View(userUpdateVM);
            }
            
            catch (UnauthorizedAccessException exception)
            {
                this.ViewData["ErrorMessage"] = exception.Message;
                this.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return this.View(userUpdateVM);                
            }

            return RedirectToAction("Profile", "Users", new { id = id });

        }

        [Authorize]
        [HttpGet]
        public IActionResult UploadPicture()
        {
            var newProfilePic = new ProfilePicVM();
            return this.View(newProfilePic);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UploadPicture(ProfilePicVM picture)
        {       
            if (!ModelState.IsValid)
            {
                return this.View(picture);
            }

            string loggedUsername = User.Claims.FirstOrDefault(claim => claim.Type == "Username").Value;
            picture.FileName = $"{loggedUsername}{DateTime.Now.Ticks}.jpg";

            string profilePicDirectory = "wwwroot/ProfilePictures";
            bool hasProfilePic = Directory.EnumerateFiles(profilePicDirectory, $"{loggedUsername}*").Any();

            if (hasProfilePic)
            {
                var directory = new DirectoryInfo(@"wwwroot/ProfilePictures");
                var fileInfo = directory.GetFiles("*" + loggedUsername + "*.*");
                string profilePicName = fileInfo[0].Name;
                string oldProfilePicPath = Path.Combine("wwwroot/ProfilePictures", profilePicName);
                System.IO.File.Delete(oldProfilePicPath);

            }

            string newPicPath = Path.Combine("wwwroot/ProfilePictures", picture.FileName);
            using (var fileStream = new FileStream(newPicPath, FileMode.Create)) 
            {
                picture.ProfilePicture.CopyTo(fileStream);
            }
            
            return RedirectToAction("Profile", "Users", new { id = loggedUsername });
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
                var newUserDto = UserMapper.MapCreateDTO(userCreateVM);
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
                var token = SecurityServices.CreateApiToken(loginVM.Username, loginVM.Password);
                Response.Cookies.Append("Cookie_JWT", token);

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
            Response.Cookies.Delete("Cookie_JWT");            
            return this.View("LogoutPage");
        }
    }
}
