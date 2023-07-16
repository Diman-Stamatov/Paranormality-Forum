using System.Collections.Generic;
using System;

namespace ForumSystemTeamFour.Models.DTOs.ThreadDTOs
{
    public class ShortThreadResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }        

        public int Replies { get; set; }

        public List<string> Tags { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public int AuthorId { get; set; }

        public string AuthorUserName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
