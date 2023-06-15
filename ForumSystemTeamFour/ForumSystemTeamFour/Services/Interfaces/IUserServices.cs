using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Services.Interfaces
{
    public interface IUserServices
    {
        List<UserResponseDto> FilterBy(string login, UserQueryParameters filterParameters);
        User GetByUsername(string username);
        User GetById(int id);
        User Create(UserCreateDto userDto);
        User Update(string login, int idToUpdate, UserUpdateDto updateData);
        User Delete(string login, int idToDelete);
        User Block(string login, int idToBlock);
        User Unblock(string login, int idToUnblock);
        User PromoteToAdmin(string login, int idToPromote);
        User DemoteFromAdmin(string login, int idToDemote);
    }
}
