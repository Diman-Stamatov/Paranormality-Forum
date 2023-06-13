using System.Collections.Generic;
using System.Linq;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Repositories.Interfaces;

namespace ForumSystemTeamFour.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        static int nextId = 1;
        private  List<Tag> tags;

        public TagsRepository()
        {
            tags = new List<Tag>()
            {
                new Tag{Id = nextId++, Name = "Ufo"},
                new Tag{Id = nextId++, Name = "Skinwalker"},
                new Tag{Id = nextId++, Name = "Bigfoot"},
            };
        }
        public Tag Create(string name)
        {
            var newTag = new Tag
            {
                Id = nextId++,
                Name = name
            };
            tags.Add(newTag);
            return newTag;
        }

        public Tag Delete(int id)
        {
            var tagToDelete = this.GetById(id);
            tags.Remove(tagToDelete);
            return tagToDelete;
        }

        public List<Tag> GetAll()
        {
            return this.tags;
        }

        public Tag GetById(int id)
        {
            return tags.FirstOrDefault(tag => tag.Id == id);            
        }

        public Tag GetByName(string name)
        {
            return tags.FirstOrDefault(tag => tag.Name == name);            
        }
    }
}
