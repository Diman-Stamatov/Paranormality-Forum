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

        public User Delete(string username)
        {
            //ToDo Validation
            var userToDelete = this.GetByUsername(username);
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
        
        public User GetByUsername(string username)
        {
            var foundUser = this.users.FirstOrDefault(user=>user.Username == username);
            return foundUser ?? throw new EntityNotFoundException($"No user with the Username \"{0}\" exists on the forum!");
        }

        public User Update(string username, UserUpdateData updateData)
        {
            //ToDo Validation
            CheckDuplicateUsername(updateData.Username);
            CheckDuplicateEmail(updateData.Email);

            var userToUpdate = this.GetByUsername(username);
            userToUpdate.FirstName = updateData.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = updateData.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = updateData.Email ?? userToUpdate.Email;
            userToUpdate.Username = updateData.Username ?? userToUpdate.Username;            
            userToUpdate.Password = updateData.Password ?? userToUpdate.Password;
            
            if (updateData.PhoneNumber!=null)
            {
                //ToDo Validation
                userToUpdate.PhoneNumber = updateData.PhoneNumber;
            }
            
            return userToUpdate;
        }        

        public User PromoteToAdmin(string username)
        {
            var userToPromote = this.GetByUsername(username);
            if (userToPromote == null)
            {
                throw new EntityNotFoundException($"\"{username}\" is not a member of the forum!");
            }
            if (userToPromote.IsAdmin==true)
            {
                throw new InvalidUserInputException($"\"{username}\" is already an Administrator!");
            }
            //ToDo Validation
            userToPromote.IsAdmin = true;
            return userToPromote;
        }

        public User DemoteFromAdmin(string username)
        {
            var userToDemote = this.GetByUsername(username);
            if (userToDemote == null)
            {
                throw new EntityNotFoundException($"\"{username}\" is not a member of the forum!");
            }
            if (userToDemote.IsAdmin == false)
            {
                throw new InvalidUserInputException($"\"{username}\" is already a basic user!");
            }
            //ToDo Validation
            userToDemote.IsAdmin = false;
            return userToDemote;
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

        public User Block(string username)
        {
            var userToBlock = this.GetByUsername(username);
            if (userToBlock == null)
            {
                throw new EntityNotFoundException($"\"{username}\" is not a member of the forum!");
            }
            if (userToBlock.Blocked == true)
            {
                throw new InvalidUserInputException($"\"{username}\" is already blocked!");
            }
            //ToDo Validation
            userToBlock.Blocked = true;
            return userToBlock;
        }

        public User Unblock(string username)
        {
            var userToUnblock = this.GetByUsername(username);
            if (userToUnblock == null)
            {
                throw new EntityNotFoundException($"\"{username}\" is not a member of the forum!");
            }
            if (userToUnblock.Blocked == false)
            {
                throw new InvalidUserInputException($"\"{username}\" hasn't been blocked!");
            }
            //ToDo Validation
            userToUnblock.Blocked = false;
            return userToUnblock;
        }
    }
}
