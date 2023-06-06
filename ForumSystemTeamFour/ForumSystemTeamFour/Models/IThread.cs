namespace ForumSystemTeamFour.Models
{
    public interface IThread :IPost
    {
        string Title { get; set; }
        List<int> Replies { get; set; }
        List<int> Tags { get; set; }
    }
}
