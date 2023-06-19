using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTeamFour.Tests.MockModels
{
    internal static class TestModels
    {
        const string ValidFirstName = "FirstName";
        const string ValidLastName = "LastName";
        const string ValidEmail = "valid@email.com";
        const string ValidUsername = "ValidUsername";
        const string ValidPassword = "ValidPassword";
        const string ValidPhoneNumber = "0888 123 456";



        public static User GetTestUser(int id)
        {
            return new User
            {
                Id = id,
                FirstName = ValidFirstName + id,
                LastName = ValidLastName + id,
                Email = ValidEmail + id,
                Username = ValidUsername + id,
                Password = ValidPassword + id
            };
        }

        //UsersRepository

        //UserMapper

        public static Mock<ISecurityServices> GetInvalidAuthenticationSecurity()
        {
            var mockSecurity = new Mock<ISecurityServices>();

            mockSecurity.Setup(security => security.CheckAdminAuthorization(It.IsAny<User>()))
                .Throws<UnauthorizedAccessException>();

            mockSecurity.Setup(security => security.CheckAuthorAuthorization(It.IsAny<User>(), It.IsAny<IPost>()))
                .Throws<UnauthorizedAccessException>();

            mockSecurity.Setup(security => security.CheckUserAuthorization(It.IsAny<User>(), It.IsAny<User>()))
                .Throws<UnauthorizedAccessException>();

            return mockSecurity;
        }
        public static Mock<ISecurityServices> GetValidAuthenticationSecurity()
        {
            var mockSecurity = new Mock<ISecurityServices>();
            mockSecurity.Setup(security => security.CheckAdminAuthorization(It.IsAny<User>()));

            mockSecurity.Setup(security => security.CheckAuthorAuthorization(It.IsAny<User>(), It.IsAny<IPost>()));

            mockSecurity.Setup(security => security.CheckUserAuthorization(It.IsAny<User>(), It.IsAny<User>()));

            return mockSecurity;
        }
    }
}
