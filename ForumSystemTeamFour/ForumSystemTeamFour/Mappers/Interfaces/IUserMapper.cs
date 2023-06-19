using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(UserCreateDto userDto);
        UserResponseDto Map(User user);
        List<UserResponseDto> Map(List<User> users);
    }
}
