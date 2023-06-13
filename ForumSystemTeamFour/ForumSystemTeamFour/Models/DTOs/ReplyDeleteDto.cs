using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyDeleteDto
    {
        [Required]
        public int? ThreadId { get; set; }
        [Required]
        public int? ReplyId { get; set; }
        [Required]
        public int? AuthorId { get; set; }
    }
}
