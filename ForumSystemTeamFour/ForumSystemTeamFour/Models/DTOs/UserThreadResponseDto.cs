using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class UserThreadResponseDto
    {
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public string Author { get; set; }
        public int NumberOfReplies { get; set; }
        public List<string> Tags { get; set; }
    }
}
