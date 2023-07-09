using System.Collections.Generic;
using ForumSystemTeamFour.Models;

namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class ThreadQueryParameters
    {
        public string CreationDate { get; set; }
        public Thread Тhread { get; set; }
        public User Author { get; set; }
        public List<Tag> Tags { get; set; }
        public string CreatedAfter { get; set; }
        public string CreatedBefore { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageSize { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}
