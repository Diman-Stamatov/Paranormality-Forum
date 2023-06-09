using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour.Services
{
    public class UserServices : IUserServices
    {
        private readonly UsersRepository repository;

        public UserServices(UsersRepository repository) 
        {
            this.repository = repository;
        }

        public User Create(User user)
        {
           return this.repository.Create(user);
        }

        public User Delete(int id)
        {
           return this.repository.Delete(id);
        }

        public List<User> FilterBy(UserQueryParameters filterParameters)
        {
            return this.repository.FilterBy(filterParameters);
        }

        public List<User> GetAll()
        {
            return this.repository.GetAll();
        }

        public User GetById(int id)
        {
            return this.repository.GetById(id);
        }
        public User GetByUsername(string username)
        {
            return this.repository.GetByUsername(username);
        }
        public User GetByEmail(string email)
        {
            return this.repository.GetByEmail(email);
        }
        public User Update(int id, User user)
        {
            return this.repository.Update(id, user);
        }
    }
}
