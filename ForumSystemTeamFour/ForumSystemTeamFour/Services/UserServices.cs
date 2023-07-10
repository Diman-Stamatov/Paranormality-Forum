using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
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
        public User GetByUsername(string username)
        {
            return repository.GetByUsername(username);
        }

        public UserResponseDto Block(int loggedUserId, int idToBlock)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);
            var blockedUser = this.repository.Block(idToBlock);

            return userMapper.Map(blockedUser);
        }

        public UserResponseDto Create(UserCreateDto userDto)
        {
            var user = this.userMapper.Map(userDto);
            user.Password = this.forumSecurity.EncodePassword(userDto.Password);
            var createdUser = this.repository.Create(user);

            return userMapper.Map(createdUser);
        }

        public UserResponseDto Delete(int loggedUserId, int idToDelete)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            var userToDelete = this.repository.GetById(idToDelete);
            forumSecurity.CheckUserAuthorization(loggedUser, userToDelete);

            var deletedUser = this.repository.Delete(userToDelete);
            return userMapper.Map(deletedUser);
        }

        public UserResponseDto DemoteFromAdmin(int loggedUserId, int idToDemote)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);
            var demotedUser = this.repository.DemoteFromAdmin(idToDemote);

            return userMapper.Map(demotedUser);
        }

        public List<UserResponseDto> FilterBy(int loggedUserId, UserQueryParameters filterParameters)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            var filteredUsers = this.repository.FilterBy(loggedUser, filterParameters);

            return userMapper.Map(filteredUsers);
        }        

        public UserResponseDto PromoteToAdmin(int loggedUserId, int idToPromote)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);
            var promotedUser = this.repository.PromoteToAdmin(idToPromote);

            return userMapper.Map(promotedUser);
        }

        public UserResponseDto Unblock(int loggedUserId, int idToUnblock)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            forumSecurity.CheckAdminAuthorization(loggedUser);
            var unblockedUser = this.repository.Unblock(idToUnblock);

            return userMapper.Map(unblockedUser);
        }

        public UserResponseDto Update(int loggedUserId, int idToUpdate, UserUpdateDto updateData)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            var userToUpdate = this.repository.GetById(idToUpdate);
            forumSecurity.CheckUserAuthorization(loggedUser, userToUpdate);
            var updatedUser = this.repository.Update(userToUpdate, updateData);

            return userMapper.Map(updatedUser);
        }

        public UserResponseDto Update(int loggedUserId, string usernameToUpdate, UserUpdateDto updateData)
        {
            var loggedUser = this.repository.GetById(loggedUserId);
            var userToUpdate = this.repository.GetByUsername(usernameToUpdate);
            forumSecurity.CheckUserAuthorization(loggedUser, userToUpdate);
            var updatedUser = this.repository.Update(userToUpdate, updateData);

            return userMapper.Map(updatedUser);
        }
    }
}
