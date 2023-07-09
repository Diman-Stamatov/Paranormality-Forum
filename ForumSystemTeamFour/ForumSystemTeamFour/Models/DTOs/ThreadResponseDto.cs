using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ThreadResponseDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public bool isDeleted { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public List<ReplyReadDto> Replies { get; set; }

        public List<Tag> Tags { get; set; }

        public UserResponseDto Author { get; set; }
    }
}
