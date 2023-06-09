using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Repositories
{
    public class AdminsRepository : IAdminsRepository
    {
        private List<Admin> admins;

        public AdminsRepository()
        {
            admins = new List<Admin>();
        }
        public Admin Create(User user)
        {
            var duplicateAdmin = this.GetById(user.Id);
            if (duplicateAdmin != null)
            {
                throw new InvalidOperationException($"The user {user.Username} is already an Administrator!");//ToDo Custom Exceptions
            }
            var newAdmin = new Admin()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };

            this.admins.Add(newAdmin);
            return newAdmin;
        }
        public Admin Demote(int id)
        {
            var adminToDemote = this.GetById(id);
            if (adminToDemote == null)
            {
                throw new InvalidOperationException($"The user with ID number {id} is not an Administrator!");//ToDo Custom Exceptions
            }
            this.admins.Remove(adminToDemote);
            return adminToDemote;
        }

        public List<Admin> GetAll()
        {
            return this.admins;
        }

        public Admin GetById(int id)
        {
            return this.admins.FirstOrDefault(admin => admin.Id == id);
        }

        public Admin GetByUsername(string username)
        {
            return this.admins.FirstOrDefault(admin => admin.Username == username);
        }
    }
}
