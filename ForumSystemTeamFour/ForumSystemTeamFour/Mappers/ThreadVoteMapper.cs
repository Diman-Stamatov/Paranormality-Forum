using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Enums;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers
{
    public class ThreadVoteMapper : IThreadVoteMapper
    {
        public ThreadVoteDto Map(ThreadVote threadVote)
        {
            return new ThreadVoteDto
            {
                Id = threadVote.Id,
                VoterUsername = threadVote.VoterUsername,
                VoteType = threadVote.VoteType.ToString()
            };
        }

        public List<ThreadVoteDto> Map(List<ThreadVote> threadVote)
        {
            var listOfThreadVoteDtos = new List<ThreadVoteDto>();
            foreach (var vote in threadVote)
            {
                listOfThreadVoteDtos.Add(this.Map(vote));

            }
            return listOfThreadVoteDtos;
        }
    }
}
