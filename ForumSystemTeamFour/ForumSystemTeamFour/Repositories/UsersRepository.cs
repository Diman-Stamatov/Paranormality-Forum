using System;
using System.Collections.Generic;
using System.Linq;
using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumSystemTeamFour.Repositories
{

    public class UsersRepository : IUsersRepository
    {        
        private readonly ForumDbContext context;

        public UsersRepository(ForumDbContext context)
        {
            this.context = context;            
        }

        public User Create(User user)        
        {
            CheckDuplicateUsername(user.Username);
            CheckDuplicateEmail(user.Email);
                        
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User Delete(User userToDelete)
        {
            userToDelete.IsDeleted = true;
            context.SaveChanges();

            return userToDelete;
        }

        public List<User> FilterBy(User loggedUser, UserQueryParameters filterParameters)
        {
            var filteredUsers = context.Users
                .Where(user=>user.IsDeleted == false)
                .Include(user => user.Threads)
                .ThenInclude(thread => thread.Tags)
                .Include(user => user.Threads)
                .ThenInclude(thread => thread.Replies)
                .ToList();

            if (!string.IsNullOrEmpty(filterParameters.FirstName))
            {
                filteredUsers = filteredUsers.FindAll(user => user.FirstName == filterParameters.FirstName);
            }

            if (!string.IsNullOrEmpty(filterParameters.LastName))
            {
                filteredUsers = filteredUsers.FindAll(user => user.LastName == filterParameters.LastName);
            }

            if (!string.IsNullOrEmpty(filterParameters.Username))
            {                
                filteredUsers = filteredUsers.FindAll(user => user.Username == filterParameters.Username);
            }

            if (!string.IsNullOrEmpty(filterParameters.Email))
            {
                if (!loggedUser.IsAdmin)
                {
                    throw new UnauthorizedAccessException("Only an administrator can search by E-mail!");
                }
                filteredUsers = filteredUsers.FindAll(user => user.Email == filterParameters.Email);
            }

            if (filterParameters.Blocked != null)
            {
                filteredUsers = filteredUsers.FindAll(user => user.IsBlocked == filterParameters.Blocked);
            }

            if (!string.IsNullOrEmpty(filterParameters.SortBy))
            {
                if (filterParameters.SortBy.Equals("firstname", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredUsers = filteredUsers.OrderBy(user => user.FirstName).ToList();
                }
                else if (filterParameters.SortBy.Equals("lastname", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredUsers = filteredUsers.OrderBy(user => user.LastName).ToList();
                }
                else if (filterParameters.SortBy.Equals("username", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredUsers = filteredUsers.OrderBy(user => user.Username).ToList(); 
                }
            }
            if (!string.IsNullOrEmpty(filterParameters.SortOrder) && filterParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
            {
                filteredUsers.Reverse();
            }
            if (filteredUsers.Count == 0)
            {
                throw new EntityNotFoundException("No users correspond to the specified search parameters!");
            }
            
            return filteredUsers;
        }

        public List<User> GetAll()
        {
            var allUsers = context.Users.Where(user=>user.IsDeleted == false)
                .Include(user => user.Threads)
                .ThenInclude(thread => thread.Tags)
                .Include(user => user.Threads)
                .ThenInclude(thread => thread.Replies)
                .ToList();
            return allUsers;
        }
        
        public User GetByUsername(string username)
        {
            //CaseInsensitive
            /*var foundUser = context.Users.FirstOrDefault(user => user.Username == username && user.IsDeleted == false);*/

            foreach (var user in this.GetAll())
            {
                if (user.Username == username)
                {
                    return user;
                }
            }

            throw new EntityNotFoundException($"No user with the Username \"{username}\" exists on the forum!");
        }
        public User GetById(int id)
        {
            var foundUser = context.Users
                .Include(user => user.Threads)
                .ThenInclude(thread => thread.Tags)
                .Include(user => user.Threads)
                .ThenInclude(thread => thread.Replies)
                .FirstOrDefault(user => user.Id == id);
            if (foundUser == null || foundUser.IsDeleted == true)
            {
                throw new EntityNotFoundException($"No user with the ID number {id} exists on the forum!");
            }
            return foundUser;
        }
        public User Update(User userToUpdate, UserUpdateDto updateData)
        {
            CheckDuplicateUsername(updateData.Username);
            CheckDuplicateEmail(userToUpdate, updateData.Email);

            userToUpdate.FirstName = updateData.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = updateData.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = updateData.Email ?? userToUpdate.Email;
            if (updateData.Username != null)
            {
                if (!userToUpdate.IsAdmin)
                {
                    throw new UnauthorizedAccessException("You cannot change your Username!");
                }
                userToUpdate.Username = updateData.Username;
            }                        
            userToUpdate.Password = updateData.Password ?? userToUpdate.Password;            
            if (updateData.PhoneNumber!=null)
            {
                if (!userToUpdate.IsAdmin)
                {
                    throw new UnauthorizedAccessException("Only administrators can specify a Phone Number!");
                }
                userToUpdate.PhoneNumber = updateData.PhoneNumber;
            }

            context.SaveChanges();
            return userToUpdate;
        }        

        public User PromoteToAdmin(int idToPromote)
        {
            var userToPromote = this.GetById(idToPromote);
            
            if (userToPromote.IsAdmin==true)
            {
                throw new InvalidUserInputException($"\"{userToPromote.Username}\" is already an Administrator!");
            }            
            userToPromote.IsAdmin = true;

            context.SaveChanges();
            return userToPromote;
        }

        public User DemoteFromAdmin(int idToDemote)
        {
            var userToDemote = this.GetById(idToDemote);
                        
            if (userToDemote.IsAdmin == false)
            {
                throw new InvalidUserInputException($"\"{userToDemote.Username}\" is already a basic user!");
            }            
            userToDemote.IsAdmin = false;
            userToDemote.PhoneNumber = null;

            context.SaveChanges();
            return userToDemote;
        }

        public User Block(int idToBlock)
        {
            var userToBlock = this.GetById(idToBlock);

            if (userToBlock.IsAdmin == true)
            {
                throw new InvalidUserInputException($"\"{userToBlock.Username}\" is an Administrator and cannot be blocked!");
            }
            if (userToBlock.IsBlocked == true)
            {
                throw new InvalidUserInputException($"\"{userToBlock.Username}\" is already blocked!");
            }            
            userToBlock.IsBlocked = true;

            context.SaveChanges();
            return userToBlock;
        }

        public User Unblock(int idToUnblock)
        {
            var userToUnblock = this.GetById(idToUnblock);            
            
            if (userToUnblock.IsBlocked == false)
            {
                throw new InvalidUserInputException($"\"{userToUnblock.Username}\" hasn't been blocked!");
            }            
            userToUnblock.IsBlocked = false;

            context.SaveChanges();
            return userToUnblock;
        }

        private void CheckDuplicateUsername(string username)
        {
            var foundUser = context.Users.FirstOrDefault(user => user.Username == username);
            if (foundUser != null)
            {
                throw new DuplicateEntityException($"The username \"{username}\" is already in use!");
            }
        }
        private void CheckDuplicateEmail(string email)
        {
            var foundUser = context.Users.FirstOrDefault(user => user.Email == email);
            if (foundUser != null)
            {
                throw new DuplicateEntityException($"The email \"{email}\" is already in use!");
            }
        }
        private void CheckDuplicateEmail(User originalUser, string email)
        {
            var foundUser = context.Users.FirstOrDefault(user => user.Email == email);
            if (foundUser != null && foundUser != originalUser)
            {
                throw new DuplicateEntityException($"The email \"{email}\" is already in use!");
            }
        }
    }
}
