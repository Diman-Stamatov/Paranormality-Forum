namespace ForumSystemTeamFour.Models
{
    public class Thread : IThread
    {
        public string Title { get; set; }
        public List<int> Replies { get; set; }
        public List<int> Tags { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
