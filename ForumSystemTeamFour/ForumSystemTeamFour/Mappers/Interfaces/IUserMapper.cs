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
        UserResponseDto MapResponseDto(User user);
        UserUpdateDto MapUpdateDTO(UserUpdateVM userUpdateVm);
        List<UserResponseDto> MapResponseDtoList(List<User> users);
        UserProfileVM MapProfileVM(User user);
        UserUpdateVM MapUpdateVM(User user);
        List<UserThreadResponseDto> MapThreadsForUser(List<Thread> threads);
    }
}
