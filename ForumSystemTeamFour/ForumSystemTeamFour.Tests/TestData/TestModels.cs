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
        public static int id = DefaultId;
        public static int nextId = id;
        public const int NamesMinLength = 4;
        public const int NamesMaxLength = 32;
        public const int ThreadTitleMinLength = 16;
        public const int ThreadTitleMaxLength = 64;
        public const int ThreadContentMinLength = 32;
        public const int ThreadContentMaxLength = 8192;
        public const string ValidFirstName = "FirstName";
        public const string ValidLastName = "LastName";
        public const string ValidThreadTitle = "ValidThreadTitle";
        public const string ValidThreadContent = "ValidThreadContentValidThreadContent";
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

        public static List<string> GetTestListOfStrings(int lengthOfTag, int countOfTags)
        {
            var listOfStrings = new List<string>();
            for (int i = 1; i <= countOfTags; i++)
            {
                listOfStrings.Add(GetTestString(lengthOfTag));
            }
            return listOfStrings;
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
                Email = ValidEmail + id,
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
            mockSecurity.Setup(security => security.CreateToken(It.IsAny<string>()))
                .Returns(GetTestString(10));

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

        public static User GetUser()
        {
            return new User
            {
                Id = nextId++,
                FirstName = ValidFirstName + nextId,
                LastName = ValidLastName + nextId,
                Email = ValidEmail + nextId,
                Username = ValidUsername + nextId,
                Password = ValidPassword + nextId
            };
        }
        public static Models.Thread GetTestThread()
        {
            return new Models.Thread
            {
                Id = ++nextId,
                Title = ValidThreadTitle + id,
                Content = ValidThreadContent + id,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                AuthorId = DefaultId,
                Author = GetUser(),
                IsDeleted = false
            };
        }
        public static Models.Thread GetTestDefaultThread()
        {
            return new Models.Thread
            {
                Id = ++nextId,
                Title = ValidThreadTitle + id,
                Content = ValidThreadContent + id,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                AuthorId = DefaultId,
                Author = GetDefaultUser(),
                IsDeleted = false
            };
        }
        public static List<Models.Thread> GetTestThreads(int numberOfThreads)
        {
            var testThreadList = new List<Models.Thread>();

            for (int i = 1; i <= numberOfThreads; i++)
            {
                testThreadList.Add(GetTestThread());
            }

            return testThreadList;
        }

        public static ThreadCreateDto GetTestThreadCreateDto()
        {
            return new ThreadCreateDto
            {
                Title = ValidThreadTitle,
                Content = ValidThreadContent
            };
        }

        public static ThreadResponseDto GetTestThreadResponseDto()
        {
            return new ThreadResponseDto
            {
                Title = ValidThreadTitle,
                Content = ValidThreadContent,
                Likes = 0,
                Dislikes = 0,
                isDeleted = false,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now
                //ToDo Add list<ReplyReadDto> when test model for Reply is ready 
            };
        }

        public static List<ThreadResponseDto> GetTestListOfThreadResponseDto(int count)
        {
            var listOfThreadResponseDto = new List<ThreadResponseDto>();
            for (int i = 1; i <= count; i++)
            {
                listOfThreadResponseDto.Add(GetTestThreadResponseDto());
            }

            return listOfThreadResponseDto;
        }

        public static UserThreadResponseDto GetTestUserThreadResponseDto()
        {
            return new UserThreadResponseDto
            {
                Title = ValidThreadTitle,
                CreationDate = DateTime.Now.ToString(),
                Author = ValidUsername,
                NumberOfReplies = 0,
                Tags = GetTestListOfStrings(2, 2)
            };
        }

        public static List<UserThreadResponseDto> GetTestListOfUserThreadResponseDto(int count)
        {
            var listOfUserThreadResponseDto = new List<UserThreadResponseDto>();
            for (int i = 1; i < count; i++)
            {
                listOfUserThreadResponseDto.Add(GetTestUserThreadResponseDto());
            }
            return listOfUserThreadResponseDto;
        }

        public static ThreadUpdateDto GetTestThreadUpdateDto()
        {
            return new ThreadUpdateDto
            {
                Title = ValidThreadTitle,
                Content = ValidThreadContent,
            };
        }

        public static Mock<IThreadRepositroy> GetTestThreadRepositroy()
        {
            var mockRepository = new Mock<IThreadRepositroy>();

            mockRepository.Setup(repository => repository.Create(It.IsAny<Models.Thread>()))
                .Returns(GetTestThread());
            mockRepository.Setup(repository => repository.Delete(It.IsAny<Models.Thread>()))
                .Returns(GetTestThread());
            mockRepository.Setup(repository => repository.GetAll())
                .Returns(GetTestThreads(3));
            mockRepository.Setup(repository => repository.GetAllByUserId(It.IsAny<int>()))
                .Returns(GetTestThreads(3));
            mockRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(GetTestThread());
            mockRepository.Setup(repository => repository.Update(It.IsAny<Models.Thread>(), It.IsAny<Models.Thread>()))
                .Returns(GetTestThread());
            //ToDo  mockRepository.Setup(repository => repository.FilterBy(It.IsAny<Thread>(), It.IsAny<ThreadQueryParameters>()))
            //  .Returns(GetTestThreadList(3));           

            return mockRepository;
        }

        public static Mock<IThreadMapper> GetTestThreadMapper()
        {
            var mockMapper = new Mock<IThreadMapper>();

            mockMapper.Setup(mapper => mapper.Map(It.IsAny<ThreadCreateDto>(), It.IsAny<User>()))
                .Returns(GetTestThread());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<Models.Thread>()))
                .Returns(GetTestThreadResponseDto());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<Models.Thread>(), It.IsAny<ThreadUpdateDto>()))
                .Returns(GetTestThread());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<List<Models.Thread>>()))
                .Returns(GetTestListOfThreadResponseDto(3));
            mockMapper.Setup(mapper => mapper.MapForUser(It.IsAny<List<Models.Thread>>()))
                .Returns(GetTestListOfUserThreadResponseDto(3));

            return mockMapper;
        }

        public static Mock<IThreadService> GetTestThreadService()
        {
            var mockServices = new Mock<IThreadService>();

            mockServices.Setup(services => services.Create(It.IsAny<ThreadCreateDto>(), It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.Update(It.IsAny<int>(), It.IsAny<ThreadUpdateDto>(), It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.GetAll())
                .Returns(GetTestListOfThreadResponseDto(3));
            mockServices.Setup(services => services.GetById(It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.GetAllByUserId(It.IsAny<int>()))
                .Returns(GetTestListOfThreadResponseDto(3));

            return mockServices;
        }
    }
}
