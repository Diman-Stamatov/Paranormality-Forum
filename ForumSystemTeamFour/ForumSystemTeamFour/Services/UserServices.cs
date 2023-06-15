using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Security;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;


namespace ForumSystemTeamFour.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUsersRepository repository;
        private readonly ForumSecurity forumSecurity;
        private readonly UserMapper userMapper;
        public UserServices(IUsersRepository repository, ForumSecurity forumSecurity, UserMapper userMapper)
        {
            this.repository = repository;
            this.forumSecurity = forumSecurity;
            this.userMapper = userMapper;
        }

        public User Block(string login, int idToBlock)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.Block(idToBlock);
        }

        public User Create(UserCreateDto userDto)
        {
            var user = this.userMapper.Map(userDto);
            return this.repository.Create(user);
        }

        public User Delete(string login, int idToDelete)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            var userToDelete = this.GetById(idToDelete);
            forumSecurity.CheckUserAuthorization(loggedUser, userToDelete);

            return this.repository.Delete(userToDelete);
        }

        public User DemoteFromAdmin(string login, int idToDemote)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.DemoteFromAdmin(idToDemote);
        }

        public List<UserResponseDto> FilterBy(string login, UserQueryParameters filterParameters)
        {
            var loggedUser = forumSecurity.Authenticate(login);

            return this.repository.FilterBy(loggedUser, filterParameters);
        }

        public User GetByUsername(string username)
        {
            return this.repository.GetByUsername(username);
        }

        public User GetById(int id)
        {
            return this.repository.GetById(id);
        }

        public User PromoteToAdmin(string login, int idToPromote)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.PromoteToAdmin(idToPromote);
        }

        public User Unblock(string login, int idToUnblock)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.CheckAdminAuthorization(loggedUser);

            return this.repository.Unblock(idToUnblock);
        }

        public User Update(string login, int idToUpdate, UserUpdateDto updateData)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            var userToUpdate = this.GetById(idToUpdate);
            forumSecurity.CheckUserAuthorization(loggedUser, userToUpdate);

            return this.repository.Update(userToUpdate, updateData);
        }
    }
}
