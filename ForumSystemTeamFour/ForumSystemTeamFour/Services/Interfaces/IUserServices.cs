using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels.User;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IUserServices
    {
        List<UserResponseDto> FilterBy(int loggedUserId, UserQueryParameters filterParameters);
        User GetById(int id);
        User GetByUsername(string username);
        UserProfileVM GetUserProfileVM(string username);
        UserUpdateVM GetUserUpdateVM(string username);
        UserResponseDto Create(UserCreateDto userDto);
        UserResponseDto Update(int loggedUserId, int idToUpdate, UserUpdateDto updateData);
        void Update(int loggedUserId, UserUpdateVM updateUpdateVM);
        UserResponseDto Delete(int loggedUserId, int idToDelete);
        UserResponseDto Block(int loggedUserId, int idToBlock);
        UserResponseDto Unblock(int loggedUserId, int idToUnblock);
        UserResponseDto PromoteToAdmin(int loggedUserId, int idToPromote);
        UserResponseDto DemoteFromAdmin(int loggedUserId, int idToDemote);
    }
}
