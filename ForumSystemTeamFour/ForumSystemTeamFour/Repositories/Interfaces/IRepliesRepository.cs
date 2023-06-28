using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IRepliesRepository
    {
        Reply Create(Reply reply);
        Reply GetById(int id);
        List<Reply> FilterBy(ReplyQueryParameters filter);
        Reply Update(int id, Reply reply);
        Reply Delete(int id);
        Reply UpVote(int id, string loggedUserName);
        Reply DownVote(int id, string loggedUserName);
        Reply ChangeVote(int id, string loggedUserName);
    }
}
