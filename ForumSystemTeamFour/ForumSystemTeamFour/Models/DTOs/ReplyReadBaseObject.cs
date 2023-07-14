using System;

namespace ForumSystemTeamFour.Models.DTOs
{
    public abstract class ReplyReadBaseObject
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ThreadId { get; set; }
        public AuthorDto Author { get; set; }
        public string Content { get; set; }

        public ReplyReadBaseObject(Reply reply)
        {
            Id = reply.Id;
            ThreadId = (int)reply.ThreadId;
            CreationDate = reply.CreationDate;
            ModificationDate = reply.ModificationDate;
            Author = new AuthorDto() { UserName = reply.Author.Username, Email = reply.Author.Email };
            Content = reply.Content;
        }
    }
}
