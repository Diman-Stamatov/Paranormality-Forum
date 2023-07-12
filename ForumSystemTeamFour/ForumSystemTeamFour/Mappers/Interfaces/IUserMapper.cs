using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(UserCreateDto userDto);
        UserCreateDto MapCreateDTO(UserCreateVM userVM);
        UserResponseDto MapresponseDTO(User user);
        UserUpdateDto MapUpdateDTO(UserUpdateVM updatedUser);
        List<UserResponseDto> MapresponseDTOList(List<User> users);
        UserProfileVM MapProfileVM(User user);
        UserUpdateVM MapUpdateVM(User user);
        List<UserThreadResponseDto> MapThreadsForUser(List<Thread> threads);
    }
}
