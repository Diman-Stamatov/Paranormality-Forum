namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IReply : IPost
    {
        int ThreadId { get; set; }
    }
}
