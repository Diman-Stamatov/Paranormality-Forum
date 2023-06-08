using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Repositories
{
    
    public class UsersRepository : IUsersRepository
    {
        static int NextId = 1;
        public List<User> users;

        /*public EmailRepository emailRepository;
        public UsernameRepository usernameRepository;*/
        /*public UsersRepository(EmailRepository emailRepository, UsernameRepository usernameRepository) 
        { 
            this.emailRepository = emailRepository;
            this.usernameRepository = usernameRepository;
        }*/

        public User Create(User user)
        {
            user.UserID = NextId++;
            this.users.Add(user);
            return user;
        }

        public User Delete(int id)
        {
            var userToDelete = this.GetById(id);
            this.users.Remove(userToDelete);
            return userToDelete;
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
            var foundUser =  this.users.FirstOrDefault(user=>user.UserID == id);
            return foundUser ?? throw new InvalidOperationException($"No user with ID exists on the forum!");
        }

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public User Update(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
