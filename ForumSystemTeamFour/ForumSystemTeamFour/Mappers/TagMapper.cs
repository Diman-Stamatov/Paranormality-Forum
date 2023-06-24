using ForumSystemTeamFour.Models;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers
{
    public class TagMapper
    {
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
