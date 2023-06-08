namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface IUser
    {
        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int EmailId { get; set; }
        int UsernameId { get; set; }
        string Password { get; set; }
        bool Blocked { get; set; }
    }
}
