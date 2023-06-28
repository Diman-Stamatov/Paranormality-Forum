using System;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IPost
    {
        
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        DateTime ModificationDate { get; set; }
        int AuthorId { get; set; }
        User Author { get; set; }
        string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        bool IsDeleted { get; set; }
    }
}
