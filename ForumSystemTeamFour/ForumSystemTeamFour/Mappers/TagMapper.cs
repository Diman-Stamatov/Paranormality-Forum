using ForumSystemTeamFour.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ForumSystemTeamFour.Mappers.Interfaces;

namespace ForumSystemTeamFour.Mappers
{
    public class TagMapper :ITagMapper
    {
        public string Map(Tag tag)
        {
            return tag.Name;
        }

        public List<string> Map(List<Tag> tags)
        {
            var tagNamesList = new List<string>();
            foreach (var tag in tags)
            {
                tagNamesList.Add(tag.Name);
            }
            return tagNamesList;
        }
    }
}
