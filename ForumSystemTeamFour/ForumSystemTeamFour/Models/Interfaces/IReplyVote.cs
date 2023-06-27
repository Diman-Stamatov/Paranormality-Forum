namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IReplyVote : IVote
    {
        int ReplyId { get; set; }
    }
}
