using ForumSystemTeamFour.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystemTeamFour.Models
{
    public class User : IUser
    {
        

        public int Id { get ; set ; }

        [MaxLength(32)]
        public string FirstName { get; set; }
        
        [MaxLength(32)]
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        [MaxLength(20)]
        public string Username { get; set; }
        
        [MaxLength(40)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        
        [MaxLength(16)]
        public string PhoneNumber { get; set; }
        public bool IsBlocked { get; set; }
        public List<Thread> Threads { get; set; }
        public List<Reply> Replies { get; set; }

        public User()
        {
            Threads= new List<Thread>();
            Replies= new List<Reply>();
        }
        public override bool Equals(object otherUser)
        {
            var comparedUser = (User)otherUser;
            if (this.Id == comparedUser.Id)
            {
                return true;
            }
            return false;
        }
    }
}
