﻿using ForumSystemTeamFour.Models;
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

        public User Update(int id, User user)
        {
            return this.repository.Update(id, user);
        }
    }
}
