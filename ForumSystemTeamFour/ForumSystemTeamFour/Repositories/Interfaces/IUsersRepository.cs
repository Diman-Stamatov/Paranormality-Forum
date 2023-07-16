using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
using ForumSystemTeamFour.Models.QueryParameters;

namespace ForumSystemTeamFour.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        List<User> FilterBy(User loggedUser, UserQueryParameters filterParameters);
        User GetByUsername(string username);
        User GetById(int id);
        User Create(User user);
        User Update(User userToUpdate, UserUpdateDto updateData);
        User Block(int idToBlock);
        User Unblock(int idToUnblock);
        User PromoteToAdmin(int idToPromote); 
        User DemoteFromAdmin(int idToDemote);
        User Delete(User userToDelete);
        int GetCount();


	}
}
