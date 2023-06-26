using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IReplyMapper
    {
        // Create
        Reply Map(ReplyCreateDto replyCreateDto, User author);

        // Read
        ReplyReadDto Map(Reply reply);

        // Update
        Reply Map(Reply reply, ReplyUpdateDto replyUpdateDto);
    }
}
