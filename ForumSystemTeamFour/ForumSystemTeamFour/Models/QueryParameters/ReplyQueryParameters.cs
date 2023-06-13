using System;

namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class ReplyQueryParameters
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
