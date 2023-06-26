using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTeamFour.Tests.TestData
{
    internal static class TestModels
    {
        public const int DefaultId = 1;
        public const int NamesMinLength = 4;
        public const int NamesMaxLength = 32;
        public const string ValidFirstName = "FirstName";
        public const string ValidLastName = "LastName";        
        public const string ValidEmail = "valid@email.com";
        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 20;
        public const string ValidUsername = "ValidUsername";
        public const int PasswordMinLength = 4;
        public const int PasswordMaxLength = 20;
        public const string ValidPassword = "ValidPassword";
        public const int PhoneNumberMinLength = 4;
        public const int PhoneNumberMaxLength = 20;
        public const string ValidPhoneNumber = "0888 123 456";
        public const string ValidTagName = "TagName";

        public static string GetTestString(int length)
        {
            return new string('x', length);
        }

        public static string GetTestString(int length, char character)
        {
            return new string(character, length);
        }

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

        public static List<User> GetTestUsersList(int numberOfUsers)
        {
            var testUserList = new List<User>();

            for (int i = 1; i <= numberOfUsers; i++)
            {
                testUserList.Add(GetTestUser(i));
            }

            return testUserList;
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
            blockedUser.IsBlocked = true;
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

        public static UserResponseDto GetTestAdminResponseDto()
        {
            var defaultDto = GetTestUserResponseDto(DefaultId);
            defaultDto.IsAdmin = true;

            return defaultDto;
        }

        public static UserResponseDto GetTestBlockedUserResponseDto()
        {
            var defaultDto = GetTestUserResponseDto(DefaultId);
            defaultDto.IsBlocked = true;

            return defaultDto;
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

        public static UserUpdateDto GetTestUserUpdateDto()
        {
            return new UserUpdateDto()
            {
                FirstName = ValidFirstName,
                LastName = ValidLastName,
                Email = ValidEmail,
                Username = ValidUsername,
                Password = ValidPassword
            };
        }
        

        public static Mock<IUsersRepository> GetTestUsersRepository()
        {
            var mockRepository = new Mock<IUsersRepository>();

            mockRepository.Setup(repository => repository.Create(It.IsAny<User>()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.Delete(It.IsAny<User>()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.FilterBy(It.IsAny<User>(), It.IsAny<UserQueryParameters>()))
                .Returns(GetTestUsersList(3));
            mockRepository.Setup(repository => repository.GetAll())
                .Returns(GetTestUsersList(3));
            mockRepository.Setup(repository => repository.GetByUsername(It.IsAny<string>()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(GetTestUser(DefaultId));
            mockRepository.Setup(repository => repository.Update(GetDefaultUser(), It.IsAny<UserUpdateDto>()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.PromoteToAdmin(It.IsAny<int>()))
                .Returns(GetDefaultAdmin());
            mockRepository.Setup(repository => repository.DemoteFromAdmin(It.IsAny<int>()))
                .Returns(GetDefaultUser());
            mockRepository.Setup(repository => repository.Block(It.IsAny<int>()))
                .Returns(GetBlockedUser());
            mockRepository.Setup(repository => repository.Unblock(It.IsAny<int>()))
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

        public static Mock<IUserServices> GetTestUserServices()
        {
            var mockServices = new Mock<IUserServices>();

            mockServices.Setup(services => services.GetById(It.IsAny<int>()))
                .Returns(GetDefaultUser());
            mockServices.Setup(services => services.Block(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestUserResponseDto(DefaultId));
            mockServices.Setup(services => services.Create(It.IsAny<UserCreateDto>()))
                .Returns(GetTestUserResponseDto(DefaultId));
            mockServices.Setup(services => services.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestUserResponseDto(DefaultId));
            mockServices.Setup(services => services.DemoteFromAdmin(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestUserResponseDto(DefaultId));
            mockServices.Setup(services => services.FilterBy(It.IsAny<int>(), It.IsAny<UserQueryParameters>()))
                .Returns(GetTestUserResponseDtoList(3));
            mockServices.Setup(services => services.PromoteToAdmin(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestUserResponseDto(DefaultId));
            mockServices.Setup(services => services.Unblock(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestUserResponseDto(DefaultId));
            mockServices.Setup(services => services.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserUpdateDto>()))
                .Returns(GetTestUserResponseDto(DefaultId));

            return mockServices;
        }

        public static Mock<IUserMapper> GetTestUserMapper()
        {
            var mockMapper = new Mock<IUserMapper>();

            mockMapper.Setup(mapper => mapper.Map(It.IsAny<UserCreateDto>()))
                .Returns(GetDefaultUser());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<User>()))
                .Returns(GetTestUserResponseDto(DefaultId));
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
            mockSecurity.Setup(security => security.Authenticate(It.IsAny<string>()))
                .Returns(GetDefaultUser()); ;
            mockSecurity.Setup(security => security.CheckAdminAuthorization(It.IsAny<User>()));
            mockSecurity.Setup(security => security.CheckAuthorAuthorization(It.IsAny<User>(), It.IsAny<IPost>()));
            mockSecurity.Setup(security => security.CheckUserAuthorization(It.IsAny<User>(), It.IsAny<User>()));

            return mockSecurity;
        }

        public static List<UserResponseDto> MapResponseList(List<User> users)
        {
            var mapper = new UserMapper();
            var responseDtoList = new List<UserResponseDto>();
            foreach (var user in users)
            { 
                responseDtoList.Add(mapper.Map(user));
            }

            return responseDtoList;
        }
    }
}
