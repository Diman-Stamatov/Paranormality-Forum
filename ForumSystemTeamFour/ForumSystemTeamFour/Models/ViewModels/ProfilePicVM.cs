using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using ForumSystemTeamFour.CustomValidationAttributes;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class ProfilePicVM
    {
        public string FileName { set; get; }

        [Required(ErrorMessage = "Please select a picture!")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize]
        public IFormFile ProfilePicture { set; get; }
    }

    
}
