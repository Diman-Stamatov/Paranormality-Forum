using System.Collections.Generic;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Security;
using ForumSystemTeamFour.Services.Interfaces;


namespace ForumSystemTeamFour.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUsersRepository repository;
        private readonly ForumSecurity forumSecurity;

        public UserServices(IUsersRepository repository, ForumSecurity forumSecurity) 
        {
            this.repository = repository;
            this.forumSecurity = forumSecurity;
        }

        public User Block(string login, string usernameToBlock)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.AuthorizeAdmin(loggedUser);

            return this.repository.Block(usernameToBlock);
        }

        public User Create(User user)
        {
           return this.repository.Create(user);
        }

        public User Delete(string login, string usernameToDelete)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            var userToDelete = this.GetByUsername(usernameToDelete);
            forumSecurity.AuthorizeUser(loggedUser, userToDelete);

            return this.repository.Delete(usernameToDelete);
        }

        public User DemoteFromAdmin(string login, string usernameToDemote)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.AuthorizeAdmin(loggedUser);

            return this.repository.DemoteFromAdmin(usernameToDemote);
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

        public User PromoteToAdmin(string login, string usernameToPromote)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.AuthorizeAdmin(loggedUser);

            return this.repository.PromoteToAdmin(usernameToPromote);
        }

        public User Unblock(string login, string usernameToUnblock)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            forumSecurity.AuthorizeAdmin(loggedUser);

            return this.repository.Unblock(usernameToUnblock);
        }

        public User Update(string login, string usernameToUpdate, UserUpdateData updateData)
        {
            var loggedUser = forumSecurity.Authenticate(login);
            var userToUpdate = this.GetByUsername(usernameToUpdate);
            forumSecurity.AuthorizeUser(loggedUser, userToUpdate);

            return this.repository.Update(usernameToUpdate, updateData);
        }
    }
}
