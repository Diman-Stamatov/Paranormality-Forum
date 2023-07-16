using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.ViewModels;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IReplyService
    {
        ReplyReadDto Create(ReplyCreateDto replyCreateDto, int loggedUserId);
        ReplyReadDto GetById(int id);
        ReplyViewModel GetViewModelById(int id);
        List<ReplyReadDto> GetByThreadId(int id);
        List<ReplyReadDto> FilterBy(ReplyQueryParameters filterParameters);
        List<ReplyViewModel> FilterForVM(ReplyQueryParameters filterParameters);
		ReplyReadDto Update(int id, ReplyUpdateDto replyUpdateDto, int loggedUserId);
        ReplyViewModel Update(int id, ReplyViewModel replyUpdateViewModel, int loggedUserId);
        ReplyReadDto Delete(int id, int loggedUserId);
        ReplyReadDto UpVote(int id, int loggedUserId);
        ReplyReadDto DownVote(int id, int loggedUserId);
        VotesDto GetReplyVotes(int id);
    }
}
