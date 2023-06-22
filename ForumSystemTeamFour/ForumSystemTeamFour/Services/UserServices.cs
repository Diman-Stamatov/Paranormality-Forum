using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;


namespace ForumSystemTeamFour.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUsersRepository repository;
        private readonly ISecurityServices forumSecurity;
        private readonly IUserMapper userMapper;
        public UserServices(IUsersRepository repository, ISecurityServices securityServices, IUserMapper userMapper)
        {
            this.repository = repository;
            this.forumSecurity = securityServices;
            this.userMapper = userMapper;
        }

        public User GetById(int id)
        {
            return repository.GetById(id);
        }

        public User Block(int loggedUserId, int idToBlock)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);
            var blockedUser = this.repository.Block(idToBlock);
            return blockedUser;
        }

        public User Create(UserCreateDto userDto)
        {
            var user = this.userMapper.Map(userDto);
            return this.repository.Create(user);
        }

        public User Delete(int loggedUserId, int idToDelete)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            var userToDelete = this.repository.GetById(idToDelete);
            forumSecurity.CheckUserAuthorization(loggedUser, userToDelete);

            return this.repository.Delete(userToDelete);
        }

        public User DemoteFromAdmin(int loggedUserId, int idToDemote)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.DemoteFromAdmin(idToDemote);
        }

        public List<UserResponseDto> FilterBy(int loggedUserId, UserQueryParameters filterParameters)
        {
            var loggedUser = this.repository.GetById(loggedUserId);

            return this.repository.FilterBy(loggedUser, filterParameters);
        }        

        public User PromoteToAdmin(int loggedUserId, int idToPromote)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.PromoteToAdmin(idToPromote);
        }

        public User Unblock(int loggedUserId, int idToUnblock)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.Unblock(idToUnblock);
        }

        public User Update(int loggedUserId, int idToUpdate, UserUpdateDto updateData)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            var userToUpdate = this.repository.GetById(idToUpdate);
            forumSecurity.CheckUserAuthorization(loggedUser, userToUpdate);

            return this.repository.Update(userToUpdate, updateData);
        }
        
    }
}
