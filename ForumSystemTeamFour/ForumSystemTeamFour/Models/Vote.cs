using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public abstract class Vote : IVote
    {
        public int Id { get ; set ; }
        public string VoterUsername { get; set; }
        public VoteType VoteType { get; set; }
    }
}
