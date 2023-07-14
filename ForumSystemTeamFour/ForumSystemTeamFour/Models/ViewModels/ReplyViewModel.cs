using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class ReplyViewModel
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }

        [Required, StringLength(8192, MinimumLength = 32, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string Content { get; set; }
        public List<ReplyVote> Likes { get; set; }
        public List<ReplyVote> Dislikes { get; set; }
    }
}
