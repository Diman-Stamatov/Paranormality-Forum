using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public class ReplyVote : Vote, IReplyVote
    {
        public int ReplyId { get; set; }
    }
}
