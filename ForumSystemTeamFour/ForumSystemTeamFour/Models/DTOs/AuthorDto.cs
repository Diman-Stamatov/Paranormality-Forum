using System;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class AuthorDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

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
