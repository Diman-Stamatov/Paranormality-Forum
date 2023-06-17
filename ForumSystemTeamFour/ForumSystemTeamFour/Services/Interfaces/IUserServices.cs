using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IUserServices
    {
        List<UserResponseDto> FilterBy(int loggedUserId, UserQueryParameters filterParameters);
        User Create(UserCreateDto userDto);
        User Update(int loggedUserId, int idToUpdate, UserUpdateDto updateData);
        User Delete(int loggedUserId, int idToDelete);
        User Block(int loggedUserId, int idToBlock);
        User Unblock(int loggedUserId, int idToUnblock);
        User PromoteToAdmin(int loggedUserId, int idToPromote);
        User DemoteFromAdmin(int loggedUserId, int idToDemote);
    }
}
