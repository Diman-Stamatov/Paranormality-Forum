using ForumSystemTeamFour.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class UserCreateVM 
    {        
        private const string StringEmptyMessage = "Please specify a {0}!";
        private const string StringMinLengthMessage = "Your {0} must be at least {1} characters long!";        

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify a First Name!")]
        [MinLength(4, ErrorMessage = "Your First name must be at least {1} characters long!")]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify a Last name!")]
        [MinLength(4, ErrorMessage = "Your Last name must be at least {1} characters long!")]
        [MaxLength(32)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify an E-mail address!")]
        [EmailAddress(ErrorMessage = "Please specify a valid e-mail address!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = StringEmptyMessage)]
        [MinLength(4, ErrorMessage = StringMinLengthMessage)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = StringEmptyMessage)]
        [MinLength(10, ErrorMessage = StringMinLengthMessage)]
        [MaxLength(40)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your Password!")]
        [MinLength(10, ErrorMessage = "Your Password must be at least {1} characters long!")]
        [MaxLength(40)]
        public string ConfirmPassword { get; set; }
    }
}
