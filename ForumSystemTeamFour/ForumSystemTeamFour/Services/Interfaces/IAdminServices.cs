using ForumSystemTeamFour.Models;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IAdminServices
    {
        List<Admin> GetAll();
        Admin GetById(int id);
        Admin GetByUsername(string username);
        Admin Create(User user);
        Admin Demote(int id);
    }
}
