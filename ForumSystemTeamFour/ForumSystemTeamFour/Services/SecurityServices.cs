using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
        public SecurityServices(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public User Authenticate(string login)
        {
            if (login == null)
            {
                throw new BadHttpRequestException("Please provide your login information!");
            }
            var loginData = login.Split(":");

            if (loginData.Length == 1)
            {
                throw new BadHttpRequestException("Please provide your login information!");
            }
            string loginUsername = loginData[0];
            string loginPassword = loginData[1];
            var authenticatedUser = usersRepository.GetByUsername(loginUsername);
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

        public string CreateToken(string login)
        {
            var loggedUser = this.Authenticate(login);

            var builder = Program.Builder;
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes
            (builder.Configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, loggedUser.Username),
                            new Claim(JwtRegisteredClaimNames.Email, loggedUser.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString())
                         }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
