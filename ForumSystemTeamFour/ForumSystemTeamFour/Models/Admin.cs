using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public class Admin : User, IAdmin
    {
        public int? PhoneNumber { get ; set ; }
    }
}
