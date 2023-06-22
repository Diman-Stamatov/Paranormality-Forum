namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool IsAdmin { get; set; }
        string? PhoneNumber { get; set; }
        bool IsBlocked { get; set; }
    }
}
