using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class LoginVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input your username!")]       
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input your password!")]
        public string Password { get; set; }
    }
}
