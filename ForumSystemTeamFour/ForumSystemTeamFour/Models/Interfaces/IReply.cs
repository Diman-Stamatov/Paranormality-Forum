using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IReply : IPost
    {
        int ThreadId { get; set; }
        Thread Thread { get; set; }
        List<ReplyVote> Votes { get; set; }
    }
}
