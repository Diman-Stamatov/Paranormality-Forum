using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using System;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface ISecurityServices
    {
        User Authenticate(string login);
        void CheckAdminAuthorization(User user);
        void CheckAuthorAuthorization(User user, IPost post);
        void CheckUserAuthorization(User loggedUser, User targetUser);
    }
}
