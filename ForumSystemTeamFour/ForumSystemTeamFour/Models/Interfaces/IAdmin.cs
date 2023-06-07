namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IAdmin : IUser
    {
        int? PhoneNumber { get; set; }
    }
}
