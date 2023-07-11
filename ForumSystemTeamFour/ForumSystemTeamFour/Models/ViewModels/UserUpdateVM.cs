using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class UserUpdateVM
    {             

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify a First Name!")]
        [MinLength(4, ErrorMessage = "Your First name must be at least {1} characters long!")]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify a Last name!")]
        [MinLength(4, ErrorMessage = "Your Last name must be at least {1} characters long!")]
        [MaxLength(32)]
        public string LastName { get; set; }

        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify an E-mail address!")]
        [EmailAddress(ErrorMessage = "Please specify a valid e-mail address!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please specify a Password!")]
        [MinLength(4, ErrorMessage = "Your Password must be at least {1} characters long!")]
        [MaxLength(20)]        
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your Password!")]
        [MinLength(10, ErrorMessage = "Your Password must be at least {1} characters long!")]
        [MaxLength(40)]
        public string ConfirmPassword { get; set; }
        
        [MinLength(9, ErrorMessage = "Your Phone number must be at least {1} characters long!")]
        [MaxLength(16)]
        public string PhoneNumber { get; set; }

        public UserUpdateVM()
        {           
        }

        public UserUpdateVM(User user)
        {            
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.ConfirmPassword = user.Password;
            this.PhoneNumber = user.PhoneNumber;
        }


    }
}
