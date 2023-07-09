using ForumSystemTeamFour.Models;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface ITagMapper
    {
        public string Map(Tag tag);

        public List<string> Map(List<Tag> tags);
    }
}
