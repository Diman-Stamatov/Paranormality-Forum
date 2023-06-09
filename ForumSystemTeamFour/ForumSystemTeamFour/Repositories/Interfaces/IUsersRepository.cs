using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        List<User> FilterBy(UserQueryParameters filterParameters);
        User GetById(int id);
        User GetByUsername(string username);
        User GetByEmail(string email);
        User Create(User user);
        User Update(int id, User user);
        User Delete(int id);
        
    }
}
