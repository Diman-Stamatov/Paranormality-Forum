using System;

namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class ReplyQueryParameters
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CreationDate { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
