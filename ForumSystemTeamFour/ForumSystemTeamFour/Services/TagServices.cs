using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Services.Interfaces;

namespace ForumSystemTeamFour.Services
{
    public class TagServices : ITagServices
    {
        private readonly TagsRepository repository;
        public TagServices(TagsRepository repository)
        {
            this.repository = repository;
        }
        public Tag Create(string name)
        {
            return this.repository.Create(name);
        }

        public Tag Delete(int id)
        {
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
