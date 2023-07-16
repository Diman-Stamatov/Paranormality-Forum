using ForumSystemTeamFour.Models.Interfaces;
using System;

namespace ForumSystemTeamFour.Models
{
    public class ThreadVote : Vote, IThreadVote
    {
        public int ThreadId { get; set; }
    }
}
