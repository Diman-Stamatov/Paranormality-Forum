using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;

namespace ForumSystemTeamFour.Security
{
    public class ForumSecurity
    {
        private readonly IUsersRepository usersRepository;
        public ForumSecurity(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public User Authenticate(string login)
        { 
            var loginData = login.Split(":");

            if (login==null || loginData.Length == 1)
            {
                throw new BadHttpRequestException("Please provide your login information!");
            }           
            string loginUsername = loginData[0];
            string loginPassword = loginData[1];
            var authenticatedUser = this.usersRepository.GetByUsername(loginUsername);
            if (authenticatedUser.Password != loginPassword)
            {
                throw new BadHttpRequestException("The provided password is invalid!");
            }
            return authenticatedUser;
        }

        public void CheckAdminAuthorization(User user)
        {
            if (!user.IsAdmin)
            {
                throw new UnauthorizedAccessException("You are not authorized to make this change!");
            }
        }
        public void CheckAuthorAuthorization(User user, IPost post)
        {
            if (!user.IsAdmin && user.Username != post.Author.Username)
            {
                throw new UnauthorizedAccessException("You are not the author of this post!");
            }
            
        }
        public void CheckUserAuthorization(User loggedUser, User targetUser)
        {
            if (!loggedUser.IsAdmin && loggedUser.Username != targetUser.Username)
            {
                throw new UnauthorizedAccessException("You can only edit your own information!");
            }
           
        }
    }
}
