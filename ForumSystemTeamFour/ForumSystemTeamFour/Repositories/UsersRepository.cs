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

        /*private readonly EmailRepository emailRepository;
        private readonly UsernameRepository usernameRepository;
        
        public UsersRepository(EmailRepository emailRepository, UsernameRepository usernameRepository) 
        { 
            this.emailRepository = emailRepository;
            this.usernameRepository = usernameRepository;
        }*/

        public User Create(User user)        {
            
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
            var filteredList = this.users;
            if (!string.IsNullOrEmpty(filterParameters.FirstName))
            {
                filteredList = filteredList.FindAll(user => user.FirstName == filterParameters.FirstName);
            }

            if (!string.IsNullOrEmpty(filterParameters.LastName))
            {
                filteredList = filteredList.FindAll(user => user.LastName == filterParameters.LastName);
            }

            /*if (!string.IsNullOrEmpty(filterParameters.Username))
            {
                int usernameId = this.usernameRepository.GetByName(filterParameters.Username).Id;
                filteredList = filteredList.FindAll(user=>user.UserID == usernameId);
            }*/                       

            if (filterParameters.Blocked != null)
            {
                filteredList = filteredList.FindAll(user => user.Blocked == filterParameters.Blocked);
            }

            if (!string.IsNullOrEmpty(filterParameters.SortBy))
            {
                if (filterParameters.SortBy.Equals("firstname", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredList = filteredList.OrderBy(user => user.FirstName).ToList();
                }
                else if (filterParameters.SortBy.Equals("lastname", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredList = filteredList.OrderBy(user => user.LastName).ToList();
                }
                else if (filterParameters.SortBy.Equals("username", StringComparison.InvariantCultureIgnoreCase))
                {
                    /*filteredList = filteredList.OrderBy(user => user.UsernameId).ToList(); ????????????????????????????????????????????????????????*/
                }

                if (!string.IsNullOrEmpty(filterParameters.SortOrder) && filterParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredList.Reverse();
                }
            }
            return filteredList;
        }

        public List<User> GetAll()
        {
            return this.users;
        }

        public User GetById(int id)
        {
            var foundUser =  this.users.FirstOrDefault(user=>user.UserID == id);
            return foundUser ?? throw new InvalidOperationException($"No user with ID exists on the forum!"); //ToDo Custom Exceptions
        }

        public User GetByUsername(string username)
        {
            /*int usernameId = this.usernameRepository.GetByName(name).Id;
            return this.GetById(usernameId);*/
            throw new NotImplementedException();
        }

        public User Update(int id, User user)
        {
            var foundUser = this.GetById(id);
            foundUser.FirstName = user.FirstName;
            foundUser.LastName = user.LastName;
            foundUser.EmailId = user.EmailId;
            foundUser.UsernameId = user.UsernameId;            
            foundUser.Password = user.Password;
            return foundUser;
        }
    }
}
