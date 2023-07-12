using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;


namespace ForumSystemTeamFour.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUsersRepository Repository;
        private readonly ISecurityServices ForumSecurity;
        private readonly IUserMapper UserMapper;
        public UserServices(IUsersRepository repository, ISecurityServices securityServices, IUserMapper userMapper)
        {
            this.Repository = repository;
            this.ForumSecurity = securityServices;
            this.UserMapper = userMapper;
        }

        public User GetById(int id)
        {
            return Repository.GetById(id);
        }
        public User GetByUsername(string username)
        {
            return Repository.GetByUsername(username);
        }

        public UserProfileVM GetUserProfileVM(string username)
        {
            var foundUser = Repository.GetByUsername(username);
            var profileVM = UserMapper.MapProfileVM(foundUser);
            return profileVM;
        }

        public UserResponseDto Block(int loggedUserId, int idToBlock)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            ForumSecurity.CheckAdminAuthorization(loggedUser);
            var blockedUser = this.Repository.Block(idToBlock);

            return UserMapper.Map(blockedUser);
        }

        public UserResponseDto Create(UserCreateDto userDto)
        {
            var user = this.UserMapper.Map(userDto);
            user.Password = this.ForumSecurity.EncodePassword(userDto.Password);
            var createdUser = this.Repository.Create(user);

            return UserMapper.Map(createdUser);
        }

        public UserResponseDto Delete(int loggedUserId, int idToDelete)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            var userToDelete = this.Repository.GetById(idToDelete);
            ForumSecurity.CheckUserAuthorization(loggedUser, userToDelete);

            var deletedUser = this.Repository.Delete(userToDelete);
            return UserMapper.Map(deletedUser);
        }

        public UserResponseDto DemoteFromAdmin(int loggedUserId, int idToDemote)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            ForumSecurity.CheckAdminAuthorization(loggedUser);
            var demotedUser = this.Repository.DemoteFromAdmin(idToDemote);

            return UserMapper.Map(demotedUser);
        }

        public List<UserResponseDto> FilterBy(int loggedUserId, UserQueryParameters filterParameters)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            var filteredUsers = this.Repository.FilterBy(loggedUser, filterParameters);

            return UserMapper.Map(filteredUsers);
        }        

        public UserResponseDto PromoteToAdmin(int loggedUserId, int idToPromote)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            ForumSecurity.CheckAdminAuthorization(loggedUser);
            var promotedUser = this.Repository.PromoteToAdmin(idToPromote);

            return UserMapper.Map(promotedUser);
        }

        public UserResponseDto Unblock(int loggedUserId, int idToUnblock)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            ForumSecurity.CheckAdminAuthorization(loggedUser);
            var unblockedUser = this.Repository.Unblock(idToUnblock);

            return UserMapper.Map(unblockedUser);
        }

        public UserResponseDto Update(int loggedUserId, int idToUpdate, UserUpdateDto updateData)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            var userToUpdate = this.Repository.GetById(idToUpdate);
            ForumSecurity.CheckUserAuthorization(loggedUser, userToUpdate);
            var updatedUser = this.Repository.Update(userToUpdate, updateData);

            return UserMapper.Map(updatedUser);
        }

        public UserResponseDto Update(int loggedUserId, string usernameToUpdate, UserUpdateDto updateData)
        {
            var loggedUser = this.Repository.GetById(loggedUserId);
            var userToUpdate = this.Repository.GetByUsername(usernameToUpdate);
            ForumSecurity.CheckUserAuthorization(loggedUser, userToUpdate);
            var updatedUser = this.Repository.Update(userToUpdate, updateData);

            return UserMapper.Map(updatedUser);
        }
    }
}
