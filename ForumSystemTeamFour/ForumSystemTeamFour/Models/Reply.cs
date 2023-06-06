namespace ForumSystemTeamFour.Models
{
    public class Reply : IReply
    {
        public int ThreadId { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
