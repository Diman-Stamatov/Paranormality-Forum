using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour.Services
{
    public class UserServive : IUserServices
    {
        private readonly UsersRepository repository;

        public UserServive(UsersRepository repository) 
        {
            this.repository = repository;
        }

        public User Create(User user)
        {
            return this.repository.Create(user);
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> FilterBy(UserQueryParameters filterParameters)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
