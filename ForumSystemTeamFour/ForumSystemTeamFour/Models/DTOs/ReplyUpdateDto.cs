using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyUpdateDto
    {
        [Required]
        public int? ThreadId { get; set; }
        [Required]
        public int? ReplyId { get; set; }

        [Required, MinLength(32), MaxLength(8192)]
        public string Content { get; set; }
    }
}
