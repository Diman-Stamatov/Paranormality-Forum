using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IThread : IPost
    {
        public string Title { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ThreadVote> Votes { get; set; }
    }
}
