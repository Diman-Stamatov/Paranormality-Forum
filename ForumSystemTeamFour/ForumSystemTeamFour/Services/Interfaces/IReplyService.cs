using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IReplyService
    {
        Reply Create(Reply reply);
        Reply GetById(int id);
        List<Reply> FilterBy(ReplyQueryParameters filterParameters);
        Reply Update(int id, Reply reply, int userId);
        Reply Delete(int id, int userId);
        Reply UpVote(int id);
        Reply DownVote(int id);
    }
}
