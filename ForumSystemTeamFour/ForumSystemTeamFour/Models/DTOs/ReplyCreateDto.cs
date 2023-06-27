using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyCreateDto
    {
        [Required, Range(1, int.MaxValue)]
        public int? ThreadId { get; set; }

        [Required, MinLength(32), MaxLength(8192)]
        public string Content { get; set; }
    }
}
