using System;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;

namespace ForumSystemTeamFour.Models.DTOs.ThreadDTOs
{
    public class LargeThreadResponseDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool isDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public List<ReplyReadDto> Replies { get; set; }

        public List<string> Tags { get; set; }

        public List<ThreadVoteDto> Votes { get; set; }

        public UserResponseDto Author { get; set; }
    }
}
