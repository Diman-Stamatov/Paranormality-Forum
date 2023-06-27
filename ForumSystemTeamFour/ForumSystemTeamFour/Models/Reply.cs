﻿using ForumSystemTeamFour.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models
{
    public class Reply : Post, IReply
    {
        [Required, Range(1,int.MaxValue)]
        public int ThreadId { get; set; }
        [Required]
        public Thread Thread { get; set; }
    }
}
