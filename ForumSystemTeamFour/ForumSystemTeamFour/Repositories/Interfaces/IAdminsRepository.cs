using ForumSystemTeamFour.Models;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IAdminsRepository
    {
        List<Admin> GetAll();
        Admin GetById(int id);
        Admin GetByUsername(string username);
        Admin Create(User user);
        Admin Demote(int id);
    }
}
