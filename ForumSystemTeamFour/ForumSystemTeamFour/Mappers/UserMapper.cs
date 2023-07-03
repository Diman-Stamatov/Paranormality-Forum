using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.ViewModels;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IThreadMapper ThreadMapper; 
        
        public UserMapper()
        { }
        public UserMapper(IThreadMapper threadMapper)
        {
            this.ThreadMapper = threadMapper;
        }

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

        public UserCreateDto Map(UserCreateVM userVM)
        {
            return new UserCreateDto
            {
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Email = userVM.Email,
                Username = userVM.Username,
                Password = userVM.Password
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
                Threads = ThreadMapper.MapForUser(user.Threads),
                IsAdmin = user.IsAdmin,
                IsBlocked = user.IsBlocked,
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
