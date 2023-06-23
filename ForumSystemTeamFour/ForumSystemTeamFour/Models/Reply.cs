using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public class Reply : Post, IReply
    {
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }
    }
}
