using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ThreadResponcseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public List<ReplyReadDto> Comments { get; set; }

        public UserResponseDto Author { get; set; }
    }
}
