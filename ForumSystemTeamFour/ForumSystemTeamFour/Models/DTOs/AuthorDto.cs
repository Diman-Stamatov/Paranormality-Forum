using System;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class AuthorDto
    {
        [Required(), StringLength(32, MinimumLength = 4, ErrorMessage = "The {0} must be between {1} and {2} characters long.")]
        public string UserName { get; set; }

        [Required, EmailAddress, StringLength(64, MinimumLength = 4, ErrorMessage = "The {0} must be between {1} and {2} characters long.")]
        public string Email { get; set; }

        public int TotalPosts { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AuthorDto dto &&
                   UserName == dto.UserName &&
                   Email == dto.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserName, Email);
        }
    }
}
