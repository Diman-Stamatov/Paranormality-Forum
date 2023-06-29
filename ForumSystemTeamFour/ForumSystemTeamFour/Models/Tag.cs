using ForumSystemTeamFour.Models.Interfaces;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models
{
    public class Tag : ITag
    {
        public int Id { get ; set ; }
        public string Name { get; set; }
        public List<Thread> Threads { get; set; }
        public Tag() 
        { 
            Threads = new List<Thread>();
        }

        public bool IsDeleted { get; set; }
    }
}
