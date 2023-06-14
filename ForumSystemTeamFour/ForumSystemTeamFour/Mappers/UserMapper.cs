using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers
{
    public class UserMapper
    {
        

        public User Map(UserCreateDto userDto) 
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
        public UserResponseDto Map(User user)
        {
            return new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Blocked = user.Blocked,
            };
        }
        public List<UserResponseDto> Map(List<User> users)
        {
            var mappedUsers = new List<UserResponseDto>();
            foreach (var user in users)
            {
                var mappedUser = this.Map(user);
                mappedUsers.Add(mappedUser);
            }
            return mappedUsers;
        }
    }
}
