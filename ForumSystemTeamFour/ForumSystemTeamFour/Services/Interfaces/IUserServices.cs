using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IUserServices
    {
        List<UserResponseDto> FilterBy(int loggedUserId, UserQueryParameters filterParameters);
        User GetById(int id);
        UserResponseDto Create(UserCreateDto userDto);
        UserResponseDto Update(int loggedUserId, int idToUpdate, UserUpdateDto updateData);
        UserResponseDto Delete(int loggedUserId, int idToDelete);
        UserResponseDto Block(int loggedUserId, int idToBlock);
        UserResponseDto Unblock(int loggedUserId, int idToUnblock);
        UserResponseDto PromoteToAdmin(int loggedUserId, int idToPromote);
        UserResponseDto DemoteFromAdmin(int loggedUserId, int idToDemote);
    }
}
