using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.DTOs
{
    public class ReplyReadDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ThreadId { get; set; }
        public AuthorDto Author { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ReplyReadDto dto &&
                   Id == dto.Id &&
                   CreationDate == dto.CreationDate &&
                   ModificationDate == dto.ModificationDate &&
                   ThreadId == dto.ThreadId &&
                   Author.Equals(dto.Author) &&
                   Content == dto.Content &&
                   Likes == dto.Likes &&
                   Dislikes == dto.Dislikes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CreationDate, ModificationDate, ThreadId, Author, Content, Likes, Dislikes);
        }
    }
}
