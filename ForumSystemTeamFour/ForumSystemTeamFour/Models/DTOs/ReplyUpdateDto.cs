using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyUpdateDto
    {
        //[Required, Range(1, int.MaxValue)]
        //public int? ThreadId { get; set; }

        //[Required, Range(1, int.MaxValue)]
        //public int? ReplyId { get; set; }

        [Required, MinLength(32), MaxLength(8192)]
        public string Content { get; set; }
    }
}
