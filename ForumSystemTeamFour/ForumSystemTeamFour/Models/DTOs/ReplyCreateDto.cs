using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyCreateDto
    {
        [Required, Range(1, int.MaxValue)]
        public int? ThreadId { get; set; }

        [Required, StringLength(8192, MinimumLength = 32, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string Content { get; set; }
    }
}
