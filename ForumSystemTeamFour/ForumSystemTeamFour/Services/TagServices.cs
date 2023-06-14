using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Security;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour.Services
{
    public class TagServices : ITagServices
    {
        private readonly ITagsRepository repository;
        private readonly ForumSecurity forumSecurity;
        public TagServices(ITagsRepository repository, ForumSecurity forumSecurity)
        {
            this.repository = repository;
            this.forumSecurity = forumSecurity; 
        }
        public Tag Create(string name)
        {
            return this.repository.Create(name);
        }

        public Tag Delete(string login, int id)
        {
           var loggedUser = this.forumSecurity.Authenticate(login);
           this.forumSecurity.CheckAdminAuthorization(loggedUser);
           return this.repository.Delete(id);
        }

        public List<Tag> GetAll()
        {
            return this.repository.GetAll();
        }

        public Tag GetById(int id)
        {
            return this.repository.GetById(id);
        }

        public Tag GetByName(string name)
        {
            return this.repository.GetByName(name); 
        }
    }
}
