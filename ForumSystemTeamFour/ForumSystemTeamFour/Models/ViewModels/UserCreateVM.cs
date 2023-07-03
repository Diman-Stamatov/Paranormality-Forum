using ForumSystemTeamFour.Models.DTOs;
using System.Runtime.CompilerServices;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class UserCreateVM : UserCreateDto
    {
        public UserCreateVM() 
        {
            FirstName = "Jonh";
            LastName = "Doe";
            Email = "John@doe.com";
            Username = "Doejon69";
            Password = "123";
        }
    }
}
