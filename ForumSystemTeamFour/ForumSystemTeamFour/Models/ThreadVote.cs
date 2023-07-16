using ForumSystemTeamFour.Models.Interfaces;
using System;

namespace ForumSystemTeamFour.Models
{
    public class ThreadVote : Vote, IThreadVote, IEquatable<ThreadVote>
    {
        public int ThreadId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ThreadVote);
        }

        public bool Equals(ThreadVote other)
        {
            return other is not null &&
                   VoterUsername == other.VoterUsername &&
                   VoteType == other.VoteType &&
                   ThreadId == other.ThreadId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VoterUsername, VoteType, ThreadId);
        }
    }
}
