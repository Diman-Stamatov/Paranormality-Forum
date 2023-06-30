using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class VotesDto
    {
        public List<string> Likes { get; set; }
        public List<string> Dislikes { get; set; }
    }
}
