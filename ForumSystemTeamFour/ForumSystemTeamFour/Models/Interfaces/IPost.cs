using System;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IPost
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        int? AuthorId { get; set; }
        User Author { get; set; }
        string Content { get; set; }
        int Likes { get; set; }
        int Dislikes { get; set; }
    }
}
