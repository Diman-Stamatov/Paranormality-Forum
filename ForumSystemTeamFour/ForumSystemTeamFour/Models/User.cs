using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public class User : IUser
    {
        public int UserID { get ; set ; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmailId { get; set; } 
        public int UsernameId { get; set; } 
        public string Password { get; set; }
        public bool Blocked { get; set; }
    }
}
