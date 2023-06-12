﻿using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminsRepository repository;

        public AdminServices(IAdminsRepository repository)
        {
            this.repository = repository;
        }
        public Admin Create(User user)
        {
            return this.repository.Create(user);
        }

        public Admin Demote(int id)
        {
            return this.repository.Demote(id);
        }

        public List<Admin> GetAll()
        {
            return this.repository.GetAll();
        }

        public Admin GetById(int id)
        {
            return this.repository.GetById(id);
        }

        public Admin GetByUsername(string username)
        {
            return this.repository.GetByUsername(username);
        }
    }
}
