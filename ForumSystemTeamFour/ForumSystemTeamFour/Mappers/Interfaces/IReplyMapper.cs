using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels.Reply;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IReplyMapper
    {
        // Create
        Reply Map(ReplyCreateDto replyCreateDto, User author);

        // Read
        ReplyReadDto Map(Reply reply);
        ReplyViewModel MapViewModel(Reply reply);
        List<ReplyReadDto> Map(List<Reply> replies);

        // Update
        Reply Map(Reply reply, ReplyUpdateDto replyUpdateDto);
        Reply MapViewModel(Reply reply, ReplyViewModel replyUpdateViewModel);
        ReplyQueryParameters MapViewQuery(ReplyQueryParametersVM queryVM);
    }
}
