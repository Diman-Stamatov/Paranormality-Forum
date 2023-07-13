using System.Collections.Generic;
using ForumSystemTeamFour.Models;

namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class ThreadQueryParameters
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string CreationDate { get; set; }
        public string CreatedAfter { get; set; }
        public string CreatedBefore { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
    }
}
