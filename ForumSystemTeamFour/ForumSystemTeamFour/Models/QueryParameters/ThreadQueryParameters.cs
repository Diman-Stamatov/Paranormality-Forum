namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class ThreadQueryParameters
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CreationDate { get; set; }
        public string CreatedAfter { get; set; }
        public string CreatedBefore { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
