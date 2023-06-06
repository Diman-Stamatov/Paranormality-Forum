namespace ForumSystemTeamFour.Models
{
    public interface IAdmin :IUser
    {
        int? PhoneNumber { get; set; }
    }
}
