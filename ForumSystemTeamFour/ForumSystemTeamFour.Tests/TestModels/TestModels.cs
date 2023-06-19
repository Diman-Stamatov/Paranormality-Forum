using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
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
        const int DefaultId = 1;
        const string ValidFirstName = "FirstName";
        const string ValidLastName = "LastName";
        const string ValidEmail = "valid@email.com";
        const string ValidUsername = "ValidUsername";
        const string ValidPassword = "ValidPassword";
        const string ValidPhoneNumber = "0888 123 456";
        const string ValidTagName = "TagName";

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
        public static User GetDefaultUser()
        {
            return new User
            {
                Id = DefaultId,
                FirstName = ValidFirstName,
                LastName = ValidLastName,
                Email = ValidEmail,
                Username = ValidUsername,
                Password = ValidPassword
            };
        }
        public static User GetDefaultAdmin()
        {
            var newAdmin = GetDefaultUser();
            newAdmin.IsAdmin = true;
            newAdmin.PhoneNumber = ValidPhoneNumber;
            return newAdmin;
        }
        public static User GetBlockedUser()
        {
            var blockedUser = GetDefaultUser();
            blockedUser.Blocked = true;
            return blockedUser;
        }

        public static Tag GetTestTag(int id)
        {
            return new Tag { Id = id, Name = ValidTagName + id };
            
        }
        public static List<Tag> GetTestTagsList(int numberOfTags)
        {
            var tagsList = new List<Tag>();

            for (int i = 1; i <= numberOfTags; i++)
            {
                tagsList.Add(GetTestTag(i));
            }

            return tagsList;

        }
        public static UserCreateDto GetTestUserCreateDto()
        {
            return new UserCreateDto
            {
                FirstName = ValidFirstName,
                LastName = ValidLastName,
                Email = ValidEmail,
                Username = ValidUsername,
                Password = ValidPassword
            };
        }
        public static UserResponseDto GetTestUserResponseDto(int extraChar)
        {
            return new UserResponseDto
            {
                FirstName = ValidFirstName + extraChar,
                LastName = ValidLastName + extraChar,
                Email = ValidEmail + extraChar,
                Username = ValidUsername + extraChar
            };
        }
        public static List<UserResponseDto> GetTestUserResponseDtoList(int numberOfDtos)
        {
            var responseDtoList = new List<UserResponseDto>();

            for (int i = 1; i <= numberOfDtos; i++)
            {
                responseDtoList.Add(GetTestUserResponseDto(i));
            }

            return responseDtoList;
        }
                       
        public static Mock<IUsersRepository> GetTestUsersRepository()
        {
            var mockRepository = new Mock<IUsersRepository>();

            mockRepository.Setup(repository => repository.Create(GetDefaultUser()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.Delete(GetDefaultUser()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.FilterBy(It.IsAny<User>(), It.IsAny<UserQueryParameters>()))
                .Returns(GetTestUserResponseDtoList(3));
            mockRepository.Setup(repository => repository.GetAll())
                .Returns(GetTestUserResponseDtoList(3));
            mockRepository.Setup(repository => repository.GetByUsername(ValidUsername))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.GetById(DefaultId))
                .Returns(GetTestUser(DefaultId));
            mockRepository.Setup(repository => repository.Update(GetDefaultUser(), It.IsAny<UserUpdateDto>()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.PromoteToAdmin(DefaultId))
                .Returns(GetDefaultAdmin());
            mockRepository.Setup(repository => repository.DemoteFromAdmin(DefaultId))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.Block(DefaultId))
                .Returns(GetBlockedUser());
            mockRepository.Setup(repository => repository.Unblock(DefaultId))
                .Returns(GetDefaultUser());

            return mockRepository;
        }

        public static Mock<ITagsRepository> GetTestTagsRepository()
        {
            var mockRepository = new Mock<ITagsRepository>();

            mockRepository.Setup(repository => repository.Create(ValidTagName))
                .Returns(GetTestTag('\0'));
            mockRepository.Setup(repository => repository.Delete(It.IsAny<int>()))
                .Returns(GetTestTag(DefaultId));
            mockRepository.Setup(repository => repository.GetAll())
                .Returns(GetTestTagsList(3));
            mockRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(GetTestTag(DefaultId));
            mockRepository.Setup(repository => repository.GetByName(It.IsAny<string>()))
                .Returns(GetTestTag(DefaultId));

            return mockRepository;
        }

        public static Mock<IUserMapper> GetTestUserMapper()
        {
            var mockMapper = new Mock<IUserMapper>();

            mockMapper.Setup(mapper => mapper.Map(GetTestUserCreateDto()))
                .Returns(GetDefaultUser());
            mockMapper.Setup(mapper => mapper.Map(GetDefaultUser()))
                .Returns(GetTestUserResponseDto('\0'));
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<List<User>>()))
                .Returns(GetTestUserResponseDtoList(3));

            return mockMapper;
        }

        public static Mock<ISecurityServices> GetInvalidAuthenticationTestSecurity()
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

        public static Mock<ISecurityServices> GetValidAuthenticationTestSecurity()
        {
            var mockSecurity = new Mock<ISecurityServices>();
            mockSecurity.Setup(security => security.CheckAdminAuthorization(It.IsAny<User>()));

            mockSecurity.Setup(security => security.CheckAuthorAuthorization(It.IsAny<User>(), It.IsAny<IPost>()));

            mockSecurity.Setup(security => security.CheckUserAuthorization(It.IsAny<User>(), It.IsAny<User>()));

            return mockSecurity;
        }
    }
}
