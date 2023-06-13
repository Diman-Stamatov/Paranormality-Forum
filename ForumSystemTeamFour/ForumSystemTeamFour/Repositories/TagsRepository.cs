using System.Collections.Generic;
using System.Linq;
using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ForumDbContext context;

        public TagsRepository(ForumDbContext context)
        {
            this.context = context;
        }

        public Tag Create(string name)
        {
            var newTag = new Tag{Name = name};

            context.Tags.Add(newTag);
            context.SaveChanges();

            return newTag;
        }

        public Tag Delete(int id)
        {
            var tagToDelete = this.GetById(id);

            context.Tags.Remove(tagToDelete);
            context.SaveChanges();

            return tagToDelete;
        }

        public List<Tag> GetAll()
        {
            return context.Tags.ToList() ;
        }

        public Tag GetById(int id)
        {
            return context.Tags.FirstOrDefault(tag => tag.Id == id);            
        }

        public Tag GetByName(string name)
        {
            return context.Tags.FirstOrDefault(tag => tag.Name == name);            
        }
    }
}
