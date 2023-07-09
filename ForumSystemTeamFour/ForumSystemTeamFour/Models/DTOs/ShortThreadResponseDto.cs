using System.Collections.Generic;
using System;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ShortThreadResponseDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreationDate { get; set; }

        public int Replies { get; set; }

        public List<string> Tags { get; set; }

        public string Author { get; set; }
    }
}
