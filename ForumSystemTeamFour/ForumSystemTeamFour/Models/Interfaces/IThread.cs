using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IThread : IPost
    {
        string Title { get; set; }
        List<Reply> Replies { get; set; }
        List<Tag> Tags { get; set; }
    }
}
