using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        List<User> FilterBy(UserQueryParameters filterParameters);
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
