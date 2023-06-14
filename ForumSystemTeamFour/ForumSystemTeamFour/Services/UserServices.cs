using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUsersRepository repository;

        public UserServices(IUsersRepository repository) 
        {
            this.repository = repository;
        }

        public User Block(string username)
        {
            return this.repository.Block(username);
        }

        public User Create(User user)
        {
           return this.repository.Create(user);
        }

        public User Delete(string username)
        {
           return this.repository.Delete(username);
        }

        public User DemoteFromAdmin(string username)
        {
            return this.repository.DemoteFromAdmin(username);
        }

        public List<UserResponseDto> FilterBy(UserQueryParameters filterParameters)
        {
            return this.repository.FilterBy(filterParameters);
        }

        public List<UserResponseDto> GetAll()
        {
            return this.repository.GetAll();
        }
        
        public User GetByUsername(string username)
        {
            return this.repository.GetByUsername(username);
        }

        public User PromoteToAdmin(string username)
        {
            return this.repository.PromoteToAdmin(username);
        }

        public User Unblock(string username)
        {
            return this.repository.Unblock(username);
        }

        public User Update(string username, UserUpdateData updateData)
        {
            return this.repository.Update(username, updateData);
        }
    }
}
