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
        User GetByName(string name);
        User Create(UserDto userDto);
        User Update(int id, UserDto userDto);
        User Delete(int id);
        
    }
}
