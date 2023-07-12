using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
using ForumSystemTeamFour.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly ITagMapper TagMapper; 
        
        public UserMapper()
        { }
        public UserMapper(ITagMapper tagMapper)
        {
            this.TagMapper = tagMapper;
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

        public UserCreateDto MapCreateDTO(UserCreateVM userVM)
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
        public UserResponseDto MapResponseDto(User user)
        {
            return new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Threads = this.MapThreadsForUser(user.Threads),
                IsAdmin = user.IsAdmin,
                IsBlocked = user.IsBlocked,
            };
        }

        public UserUpdateDto MapUpdateDTO(UserUpdateVM updatedUser)
        {
            return new UserUpdateDto
            {
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,

                Email = updatedUser.Email,
                Password = updatedUser.Password,
                PhoneNumber = updatedUser.PhoneNumber
            };
        }
        public List<UserResponseDto> MapResponseDtoList(List<User> users)
        {
            var mappedUsers = new List<UserResponseDto>();
            foreach (var user in users)
            {
                var mappedUser = this.MapResponseDto(user);
                mappedUsers.Add(mappedUser);
            }
            return mappedUsers;
        }

        public UserProfileVM MapProfileVM(User user)
        {
            var profileVM = new UserProfileVM();
            profileVM.Username = user.Username;
            profileVM.ThreadsCount = user.Threads.Count;
            profileVM.RepliesCount = user.Replies.Count;
            return profileVM;
        }

        public UserUpdateVM MapUpdateVM(User user)
        {
            var profileVM = new UserUpdateVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password,
                PhoneNumber = user.PhoneNumber
            };
            return profileVM;
        }

        public List<UserThreadResponseDto> MapThreadsForUser(List<Thread> threads)
        {
            var returnList = new List<UserThreadResponseDto>();
            foreach (var thread in threads)
            {
                var mappedThread = new UserThreadResponseDto
                {
                    Title = thread.Title,
                    CreationDate = thread.CreationDate.ToString(),
                    Author = thread.Author.Username,
                    NumberOfReplies = thread.Replies.Count,
                    Tags = TagMapper.Map(thread.Tags)
                };
                returnList.Add(mappedThread);
            }
            return returnList;
        }

    }
}
