using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class UserUpdateVM
    {
        private const string StringEmptyMessage = "Please specify a {0}!";
        private const string StringMinLengthMessage = "Your {0} must be at least {1} characters long!";

        [Remote("CheckEmptyString", "Validation")]
        [MinLength(4, ErrorMessage = "Your First name must be at least {1} characters long!")]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [Remote("CheckEmptyString", "Validation")]
        [MinLength(4, ErrorMessage = "Your Last name must be at least {1} characters long!")]
        [MaxLength(32)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify an E-mail address!")]
        [EmailAddress(ErrorMessage = "Please specify a valid e-mail address!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = StringEmptyMessage)]
        [MinLength(4, ErrorMessage = StringMinLengthMessage)]
        [MaxLength(20)]        
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify a Password!")]
        [MinLength(10, ErrorMessage = "Your Password must be at least {1} characters long!")]
        [MaxLength(40)]
        public string ConfirmPassword { get; set; }

        [MinLength(9, ErrorMessage = StringMinLengthMessage)]
        [MaxLength(16)]
        public string PhoneNumber { get; set; }
    }
}
