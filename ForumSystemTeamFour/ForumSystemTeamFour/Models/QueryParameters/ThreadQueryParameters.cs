using System.Collections.Generic;
using ForumSystemTeamFour.Models;

namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class ThreadQueryParameters
    {
        public int ThreadId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string CreationDate { get; set; }
        public string CreatedAfter { get; set; }
        public string CreatedBefore { get; set; }

        public string ModifiedAfter { get; set; }
        public string ModifiedBefore { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageSize { get; set; } = 500;
        public int PageNumber { get; set; } = 1;
    }
}
