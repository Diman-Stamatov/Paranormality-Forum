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
        public UserResponseDto MapresponseDTO(User user)
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
        public List<UserResponseDto> MapresponseDTOList(List<User> users)
        {
            var mappedUsers = new List<UserResponseDto>();
            foreach (var user in users)
            {
                var mappedUser = this.MapresponseDTO(user);
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
            var profileVM = new UserUpdateVM();
            profileVM.FirstName = user.FirstName;
            profileVM.LastName = user.LastName;
            profileVM.Username = user.Username;
            profileVM.Email = user.Email;
            profileVM.Password = user.Password;
            profileVM.ConfirmPassword = user.Password;
            profileVM.PhoneNumber = user.PhoneNumber;
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
