using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumSystemTeamFour.Services
{
    public class SecurityServices :ISecurityServices
    {
        private readonly IUsersRepository usersRepository;
        private readonly IConfiguration configManager;
        public SecurityServices(IUsersRepository usersRepository, IConfiguration configManager)
        {
            this.usersRepository = usersRepository;
            this.configManager = configManager;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new InvalidUserInputException("Please provide your login information!");
            }
            
            string encodedPassword = EncodePassword(password);
            var authenticatedUser = usersRepository.GetByUsername(username);
            if (authenticatedUser.Password != encodedPassword)
            {
                throw new InvalidUserInputException("The provided password is invalid!");
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
        public string CreateApiToken(string username, string password)
        {
            var loggedUser = this.Authenticate(username, password);
            var claims = new[] {
                new Claim("LoggedUserId", loggedUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loggedUser.Username),
                new Claim(JwtRegisteredClaimNames.Email, loggedUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configManager["Jwt:Issuer"],
                configManager["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string EncodePassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encodedPassword);
        }


    }
}
