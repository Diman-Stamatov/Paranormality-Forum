using ForumSystemTeamFour.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models
{
    public class Thread : Post, IThread
    {
        [MaxLength(64)]
        public string Title { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
