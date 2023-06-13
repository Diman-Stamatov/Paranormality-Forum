using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IThread : IPost
    {
        string Title { get; set; }
        List<int> Replies { get; set; }
        List<int> Tags { get; set; }
    }
}
