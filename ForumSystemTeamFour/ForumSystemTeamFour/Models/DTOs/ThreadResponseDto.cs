using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ThreadResponseDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public string Title { get; set; }

        public AuthorDto Author { get; set; }
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public bool isDeleted { get; set; }
        public List<ReplyReadDto> Comments { get; set; }

       // public UserResponseDto Author { get; set; }
    }
}
