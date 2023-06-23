using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers
{
    public class UserMapper : IUserMapper
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
                Threads = MapThreads(user.Threads),
                Blocked = user.IsBlocked,
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
        //ToDo Update later with proper Thread DTO
        public List<ThreadResponseDto> MapThreads(List<Thread> threads)
        {
            var mappedThreads = new List<ThreadResponseDto>();
            foreach (var thread in threads)
            {
                mappedThreads.Add(new ThreadResponseDto
                {
                    Title = thread.Title,
                    CreationDate = thread.CreationDate.ToString(),
                    Author = thread.Author.Username,
                    NumberOfReplies = thread.Replies.Count
                });
            }
            return mappedThreads;
        }
    }
    //ToDo Update later with proper Thread DTO
    public class ThreadResponseDto
    {
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public string Author { get; set; }
        public int NumberOfReplies { get; set; }

    }
}
