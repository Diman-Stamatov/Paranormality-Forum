namespace ForumSystemTeamFour.Models
{
    public interface IReply :IPost
    {
        int? ThreadId { get; set; }
    }
}
