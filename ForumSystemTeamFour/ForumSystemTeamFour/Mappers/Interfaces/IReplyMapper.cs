using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IReplyMapper
    {
        // Create
        Reply Map(ReplyCreateDto replyCreateDto, User author);

        // Read
        ReplyReadDto Map(Reply reply);
        List<ReplyReadDto> Map(List<Reply> replies);

        // Update
        Reply Map(Reply reply, ReplyUpdateDto replyUpdateDto);
    }
}
