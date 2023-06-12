using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Repositories
{
    
    public class UsersRepository : IUsersRepository
    {
        static int NextId = 1;
        private List<User> users;
        

        public UsersRepository()
        {
            users = new List<User>()
            {
                new User{Id = NextId++,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "passwordOne"
                },
                new User{Id = NextId++,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "passwordTwo"
                },
                new User{Id = NextId++,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "passwordThree"
                },
                new User{Id = NextId++,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "passwordFour"
                },
                new User{Id = NextId++,
                FirstName = "FirstNameFive",
                LastName = "LastNameFive",
                Username = "UsernameFive",
                Email = "FirstnameFive@Lastname.com",
                Password = "passwordFive"
                }
            };
        }

        public User Create(User user)        
        {
            CheckDuplicateUsername(user.Username);
            CheckDuplicateEmail(user.Email);
            user.Id = NextId++;            
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

            if (!string.IsNullOrEmpty(filterParameters.Username))
            {                
                filteredList = filteredList.FindAll(user => user.Username == filterParameters.Username);
            }

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
                    filteredList = filteredList.OrderBy(user => user.Username).ToList(); 
                }

                if (!string.IsNullOrEmpty(filterParameters.SortOrder) && filterParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredList.Reverse();
                }
            }
            if (filteredList.Count == 0)
            {
                throw new EntityNotFoundException("No users correspond to the specified search parameters!");
            }
            return filteredList;
        }

        public List<User> GetAll()
        {
            return this.users;
        }

        public User GetById(int id)
        {
            var foundUser =  this.users.FirstOrDefault(user=>user.Id == id);
            return foundUser ?? throw new EntityNotFoundException($"No user with the ID number {0} exists on the forum!");
        }

        public User GetByUsername(string username)
        {
            var foundUser = this.users.FirstOrDefault(user=>user.Username == username);
            return foundUser ?? throw new EntityNotFoundException($"No user with the Username \"{0}\" exists on the forum!");
        }
        public User GetByEmail(string email)
        {
            var foundUser = this.users.FirstOrDefault(user => user.Email == email);
            return foundUser ?? throw new EntityNotFoundException($"No user with the E-mail \"{0}\" exists on the forum!"); 
        }

        public User Update(int id, User user)
        {
            CheckDuplicateUsername(user.Username);
            CheckDuplicateEmail(user.Email);

            var userToUpdate = this.GetById(id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.Username = user.Username;            
            userToUpdate.Password = user.Password;

            return userToUpdate;
        }        

        private void CheckDuplicateUsername(string username)
        {
            var foundUser = this.users.FirstOrDefault(user => user.Username == username);
            if (foundUser != null)
            {
                throw new DuplicateEntityException($"The username \"{username}\" is already in use!"); 
            }
        }
        private void CheckDuplicateEmail(string email)
        {
            var foundUser = this.users.FirstOrDefault(user => user.Email == email);
            if (foundUser != null)
            {
                throw new DuplicateEntityException($"The email \"{email}\" is already in use!"); 
            }
        }
    }
}
