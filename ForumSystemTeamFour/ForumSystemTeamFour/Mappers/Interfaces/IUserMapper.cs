using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;
using ForumSystemTeamFour.Models.ViewModels;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(UserCreateDto userDto);
        UserCreateDto Map(UserCreateVM userVM);
        UserResponseDto Map(User user);
        List<UserResponseDto> Map(List<User> users);
    }
}
