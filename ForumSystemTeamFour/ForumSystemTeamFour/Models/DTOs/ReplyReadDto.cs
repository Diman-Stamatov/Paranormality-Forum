using System;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyReadDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int ThreadId { get; set; }
        //public int AuthorId { get; set; }
        public AuthorDto Author { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
