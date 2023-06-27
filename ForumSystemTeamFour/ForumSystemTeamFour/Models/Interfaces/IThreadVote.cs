namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IThreadVote : IVote
    {
        int ThreadId { get; set; }
    }
}
