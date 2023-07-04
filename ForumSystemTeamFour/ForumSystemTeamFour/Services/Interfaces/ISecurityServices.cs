using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using System;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface ISecurityServices
    {
        User Authenticate(string username, string password);
        void CheckAdminAuthorization(User user);
        void CheckAuthorAuthorization(User user, IPost post);
        void CheckUserAuthorization(User loggedUser, User targetUser);
        string CreateApiToken(string username, string password);
        string EncodePassword(string password);
    }
}
