﻿using System.Collections.Generic;
using System;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;

namespace ForumSystemTeamFour.Models.DTOs.ThreadDTOs
{
    public class MediumThreadResponseDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public int RepliesCount { get; set; }

        public List<Tag> Tags { get; set; }

        public UserResponseDto Author { get; set; }
    }
}
