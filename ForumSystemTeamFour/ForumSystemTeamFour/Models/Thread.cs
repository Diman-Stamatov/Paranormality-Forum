using ForumSystemTeamFour.Models.Interfaces;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models
{
    public class Thread : Post, IThread
    {
        public string Title { get; set; }
        public List<int> Replies { get; set; }
        public List<int> Tags { get; set; }
    }
}
