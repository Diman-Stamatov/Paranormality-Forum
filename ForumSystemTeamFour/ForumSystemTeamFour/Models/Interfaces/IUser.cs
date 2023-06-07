namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IUser
    {
        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool Blocked { get; set; }
    }
}
