namespace ForumSystemTeamFour.Models.QueryParameters
{
    public class UserQueryParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool Blocked { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
