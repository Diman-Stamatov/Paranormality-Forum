using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<UserResponseDto> GetAll();
        List<UserResponseDto> FilterBy(User loggedUser, UserQueryParameters filterParameters);
        User GetByUsername(string username);        
        User Create(User user);
        User Update(string username, UserUpdateData updateData);
        User Block(string username);
        User Unblock(string username);
        User PromoteToAdmin(string username); 
        User DemoteFromAdmin(string username);
        User Delete(string username);
        
    }
}
