using ForumSystemTeamFour.Models.Interfaces;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models
{
    public class Thread : Post, IThread
    {
        public string Title { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
