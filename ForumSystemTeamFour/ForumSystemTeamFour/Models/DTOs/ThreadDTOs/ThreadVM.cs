using System;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
using ForumSystemTeamFour.Models.ViewModels;

namespace ForumSystemTeamFour.Models.DTOs.ThreadDTOs
{
    public class ThreadVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public bool isDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public List<ReplyViewModel> Replies { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public List<string> Tags { get; set; }

        public List<ThreadVoteDto> Votes { get; set; }

        public UserResponseDto Author { get; set; }
    }
}
