using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class UserResponseDto
    {
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }  

        public bool Blocked { get; set; }
    }
}
