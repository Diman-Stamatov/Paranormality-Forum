using ForumSystemTeamFour.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models
{
    public class Thread : Post, IThread
    {
        public string Title { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ThreadVote> Votes { get; set; }


        public Thread()
        {
            Replies = new List<Reply>();
            Tags = new List<Tag>();
            Votes = new List<ThreadVote>();
        }
    }
}
