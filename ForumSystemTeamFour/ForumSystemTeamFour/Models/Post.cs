using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public abstract class Post : IPost
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        
        public int AuthorId { get; set; }
        public User Author { get; set; }

        [MaxLength(8192)]    
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set ; }
        public bool IsDeleted { get; set; }
    }
}
