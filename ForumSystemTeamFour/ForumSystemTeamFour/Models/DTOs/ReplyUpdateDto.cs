using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyUpdateDto
    {
        [Required, Range(1, int.MaxValue)]
        public int? ThreadId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int? ReplyId { get; set; }

        [Required(), StringLength(8192, MinimumLength = 32, ErrorMessage = "The {0} must be between {1} and {2} characters long.")]
        public string Content { get; set; }
    }
}
