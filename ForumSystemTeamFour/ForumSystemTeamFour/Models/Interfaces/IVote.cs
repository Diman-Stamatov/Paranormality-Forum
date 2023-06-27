using ForumSystemTeamFour.Models.Enums;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IVote
    {
        int Id { get; set; }
        string VoterUsername { get; set; }
        VoteType VoteType { get; set; }
    }
}
