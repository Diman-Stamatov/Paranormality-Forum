using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public abstract class Post : IPost
    {
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set ; }
    }
}
