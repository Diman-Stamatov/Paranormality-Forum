using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IUserServices
    {
        List<UserResponseDto> FilterBy(string login, UserQueryParameters filterParameters);
        User GetByUsername(string username);
        User Create(User user);
        User Update(string login, string usernameToUpdate, UserUpdateData updateData);
        User Delete(string login, string usernameToDelete);
        User Block(string login, string usernameToBlock);
        User Unblock(string login, string usernameToUnblock);
        User PromoteToAdmin(string login, string usernameToPromote);
        User DemoteFromAdmin(string login, string usernameToDemote);
    }
}
