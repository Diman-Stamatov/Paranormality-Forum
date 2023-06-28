using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public class ThreadVote : Vote, IThreadVote
    {
        public int ThreadId { get; set; }
    }
}
