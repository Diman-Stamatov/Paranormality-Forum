﻿using System;
using System.Collections.Generic;
using System.Linq;
using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Repositories
{

    public class UsersRepository : IUsersRepository
    {
        private const string NoUserFoundMessage = "No user with the ID number {0} exists on the forum!";
        private readonly ForumDbContext context;
        private readonly UserMapper userMapper;

        public UsersRepository(ForumDbContext context, UserMapper userMapper)
        {
            this.context = context;
            this.userMapper = userMapper;
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
            context.Users.Remove(userToDelete);
            context.SaveChanges();

            return userToDelete;
        }

        public List<UserResponseDto> FilterBy(User loggedUser, UserQueryParameters filterParameters)
        {
            var filteredUsers = context.Users.ToList();
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
                filteredUsers = filteredUsers.FindAll(user => user.Blocked == filterParameters.Blocked);
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

                if (!string.IsNullOrEmpty(filterParameters.SortOrder) && filterParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    filteredUsers.Reverse();
                }
            }
            if (filteredUsers.Count == 0)
            {
                throw new EntityNotFoundException("No users correspond to the specified search parameters!");
            }
            
            return userMapper.Map(filteredUsers);
        }

        public List<UserResponseDto> GetAll()
        {
            var allUsers = context.Users.ToList();
            return userMapper.Map(allUsers);
        }
        
        public User GetByUsername(string username)
        {
            //ToDo
            /*var foundUser = context.Users.FirstOrDefault(user=>user.Username == username);*/
            User foundUser = null;
            foreach (var user in context.Users)
            {
                if (user.Username == username)
                {
                    foundUser = user;
                }
            }
            return foundUser ?? throw new EntityNotFoundException($"No user with the Username \"{username}\" exists on the forum!");
        }
        public User GetById(int id)
        {
            var foundUser = context.Users.FirstOrDefault(user => user.Id == id);
            return foundUser ?? throw new EntityNotFoundException(string.Format(NoUserFoundMessage, id));
        }
        public User Update(User userToUpdate, UserUpdateDto updateData)
        {
            CheckDuplicateUsername(updateData.Username);
            CheckDuplicateEmail(updateData.Email);

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
            
            if (userToPromote == null)
            {
                throw new EntityNotFoundException(string.Format(NoUserFoundMessage, idToPromote));
            }
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
            
            if (userToDemote == null)
            {
                throw new EntityNotFoundException(string.Format(NoUserFoundMessage, idToDemote));
            }
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
            
            if (userToBlock == null)
            {
                throw new EntityNotFoundException(string.Format(NoUserFoundMessage, idToBlock));
            }
            if (userToBlock.Blocked == true)
            {
                throw new InvalidUserInputException($"\"{userToBlock.Username}\" is already blocked!");
            }            
            userToBlock.Blocked = true;

            context.SaveChanges();
            return userToBlock;
        }

        public User Unblock(int idToUnblock)
        {
            var userToUnblock = this.GetById(idToUnblock);
            
            if (userToUnblock == null)
            {
                throw new EntityNotFoundException(string.Format(NoUserFoundMessage, idToUnblock));
            }
            if (userToUnblock.Blocked == false)
            {
                throw new InvalidUserInputException($"\"{userToUnblock.Username}\" hasn't been blocked!");
            }            
            userToUnblock.Blocked = false;

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
    }
}
