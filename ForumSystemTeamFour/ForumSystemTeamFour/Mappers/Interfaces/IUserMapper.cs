using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(UserCreateDto userDto);
        UserCreateDto Map(UserCreateVM userVM);
        UserResponseDto Map(User user);
        UserUpdateDto Map(UserUpdateVM updatedUser);
        List<UserResponseDto> Map(List<User> users);
        UserProfileVM MapProfileVM(User user);
        List<UserThreadResponseDto> MapThreadsForUser(List<Thread> threads);
    }
}
