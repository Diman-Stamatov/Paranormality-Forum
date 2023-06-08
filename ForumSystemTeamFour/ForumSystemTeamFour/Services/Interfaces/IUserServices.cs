using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IUserServices
    {
        List<User> GetAll();
        List<User> FilterBy(UserQueryParameters filterParameters);
        User GetById(int id);
        User Create(User user);
        User Update(int id, User user);
        User Delete(int id);
    }
}
