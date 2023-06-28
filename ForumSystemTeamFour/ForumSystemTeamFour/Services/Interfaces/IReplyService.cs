using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.DTOs;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IReplyService
    {
        ReplyReadDto Create(ReplyCreateDto replyCreateDto, int loggedUserId);
        ReplyReadDto GetById(int id);
        List<ReplyReadDto> FilterBy(ReplyQueryParameters filterParameters);
        ReplyReadDto Update(int id, ReplyUpdateDto replyUpdateDto, int loggedUserId);
        ReplyReadDto Delete(int id, int loggedUserId);
        ReplyReadDto UpVote(int id, int loggedUserId);
        ReplyReadDto DownVote(int id, int loggedUserId);
    }
}
