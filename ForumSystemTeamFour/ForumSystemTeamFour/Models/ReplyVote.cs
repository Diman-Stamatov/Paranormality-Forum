using ForumSystemTeamFour.Models.Interfaces;
using System;

namespace ForumSystemTeamFour.Models
{
    public class ReplyVote : Vote, IReplyVote
    {
        public int ReplyId { get; set; }
    }
}
