namespace ForumSystemTeamFour.Models.ViewModels.Reply
{
    public class ReplyQueryParametersVM
    {
        public int ThreadId { get; set; }
        public string UserName { get; set; }
        public string CreatedAfter { get; set; }
        public string CreatedBefore { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
