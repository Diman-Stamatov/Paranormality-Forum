using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ForumSystemTeamFour.Models.Comparers
{
    public class ReplyVoteComparer : IEqualityComparer<ReplyVote>
    {
        public bool Equals(ReplyVote x, ReplyVote y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] ReplyVote obj)
        {
            return obj.GetHashCode();
        }
    }
}
