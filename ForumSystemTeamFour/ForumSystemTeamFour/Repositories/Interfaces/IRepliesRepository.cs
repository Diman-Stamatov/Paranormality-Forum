using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IRepliesRepository
    {
        Reply Create(Reply reply);
        Reply GetById(int id);
        List<Reply> FilterBy(ReplyQueryParameters filterParameters);
        Reply Update(int id, Reply reply);
        Reply Delete(int id);
        Reply UpVote(int id);
        Reply DownVote(int id);
    }
}
