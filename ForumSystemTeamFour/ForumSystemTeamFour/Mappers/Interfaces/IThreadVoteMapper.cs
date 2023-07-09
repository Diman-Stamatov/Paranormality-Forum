using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IThreadVoteMapper
    {
        public ThreadVoteDto Map(ThreadVote threadVote);

        public List<ThreadVoteDto> Map(List<ThreadVote> threadVote);
    }
}
