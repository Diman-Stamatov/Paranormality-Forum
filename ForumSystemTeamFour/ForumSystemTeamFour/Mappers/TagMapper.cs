using ForumSystemTeamFour.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers
{
    public class TagMapper
    {
        public static string Map(Tag tag)
        {
            return tag.Name;
        }

        public static List<string> Map(List<Tag> tags)
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
