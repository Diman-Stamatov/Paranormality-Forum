using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ForumSystemTeamFour.Models.Comparers
{
    public class ThreadVoteComparer : IEqualityComparer<ThreadVote>
    {
        public bool Equals(ThreadVote x, ThreadVote y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] ThreadVote obj)
        {
            return obj.GetHashCode();
        }
    }
}
