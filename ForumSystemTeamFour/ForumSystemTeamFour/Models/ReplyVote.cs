using ForumSystemTeamFour.Models.Interfaces;
using System;

namespace ForumSystemTeamFour.Models
{
    public class ReplyVote : Vote, IReplyVote, IEquatable<ReplyVote>
    {
        public int ReplyId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ReplyVote);
        }

        public bool Equals(ReplyVote other)
        {
            return other is not null &&
                   VoterUsername == other.VoterUsername &&
                   VoteType == other.VoteType &&
                   ReplyId == other.ReplyId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VoterUsername, VoteType, ReplyId);
        }
    }
}
