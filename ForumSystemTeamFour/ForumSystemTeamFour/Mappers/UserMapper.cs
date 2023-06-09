using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;

namespace ForumSystemTeamFour.Mappers
{
    public class UserMapper
    {
        

        public User Map(UserDto userDto) 
        {            
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username = userDto.Username,
                Password = userDto.Password
            };
        }
    }
}
