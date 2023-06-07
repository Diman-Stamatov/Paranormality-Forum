namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IPost
    {
        int PostId { get; set; }
        DateTime CreationDate { get; set; }
        int AuthorId { get; set; }
        string Content { get; set; }
        int Likes { get; set; }
        int Dislikes { get; set; }
    }
}
