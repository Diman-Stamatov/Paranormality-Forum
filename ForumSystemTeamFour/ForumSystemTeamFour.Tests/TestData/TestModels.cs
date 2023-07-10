using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Mappers;
using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Models.Enums;
using ForumSystemTeamFour.Models.Interfaces;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories.Interfaces;
using ForumSystemTeamFour.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static ForumSystemTeamFour.Models.Enums.VoteType;
using ForumSystemTeamFour.Models.DTOs.UserDTOs;

namespace ForumSystemTeamFour.Tests.TestData
{
    internal static class TestModels
    {
        #region Constants
        public const int DefaultId = 1;
        public static int id = DefaultId;
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

        public const int ValidThreadId = 1;
        public const int InvalidThreadId = -1;

        public const int DefaultReplyId = 1;
        public const int InvalidReplyId = -1;
        public const string ValidCreationDates = "2023-01-01 2023-02-02 2023-03-03";
        public const string ValidModificationDates = "2023-01-11 2023-02-12 2023-03-13";
        public const int ContentMinLength = 32;
        public const int ContentMaxLength = 8192;

        #endregion Constants

        #region Users
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
            mockSecurity.Setup(security => security.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(GetDefaultUser()); ;
            mockSecurity.Setup(security => security.CheckAdminAuthorization(It.IsAny<User>()));
            mockSecurity.Setup(security => security.CheckAuthorAuthorization(It.IsAny<User>(), It.IsAny<IPost>()));
            mockSecurity.Setup(security => security.CheckUserAuthorization(It.IsAny<User>(), It.IsAny<User>()));
            mockSecurity.Setup(security => security.CreateApiToken(It.IsAny<string>(), It.IsAny<string>()))
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
            id++;
            return new User
            {
                FirstName = ValidFirstName + id,
                LastName = ValidLastName + id,
                Email = ValidEmail + id,
                Username = ValidUsername + id,
                Password = ValidPassword + id
            };
        }

        #endregion Users

        #region Replies
        public static List<DateTime> GetValidDates(string dates)
        {
            return dates.Split().Select(d => DateTime.Parse(d)).ToList();
        }
        public static Reply GetTestReply()
        {
            return GetTestReplyList()[0];
        }
        public static Reply GetTestReply(int replyId, DateTime creationDate, DateTime modificationDate, string content)
        {
            return new Reply()
            {
                Id = replyId,
                Author = GetDefaultUser(),
                AuthorId = DefaultId,
                CreationDate = creationDate,
                ModificationDate = modificationDate,
                ThreadId = ValidThreadId,
                Content = content,
                IsDeleted = false,
                Votes = new List<ReplyVote>()
            };
        }
        public static List<Reply> GetTestReplyList()
        {
            List<DateTime> creationDates = GetValidDates(ValidCreationDates);
            List<DateTime> modificationDates = GetValidDates(ValidModificationDates);

            return new List<Reply>
            {
                GetTestReply(1, creationDates[0], modificationDates[0], GetTestString(ContentMinLength,'a')),
                GetTestReply(2, creationDates[1], modificationDates[1], GetTestString(ContentMinLength,'b')),
                GetTestReply(3, creationDates[2], modificationDates[2], GetTestString(ContentMinLength,'c'))
            };
        }
        public static Reply GetTestUpVotedReply()
        {
            var reply = GetTestReply();
            reply.Votes.Add(GetTestVote(Like));
            return reply;
        }
        public static Reply GetTestDownVotedReply()
        {
            var reply = GetTestReply();
            reply.Votes.Add(GetTestVote(Dislike));
            return reply;
        }
        public static ReplyReadDto GetTestReplyReadDto()
        {
            return GetTestReplyReadDtoList()[0];
        }
        public static ReplyReadDto GetTestReplyReadDto(int replyId, DateTime creationDate, DateTime modificationDate, string content)
        {
            return new ReplyReadDto()
            {
                Id = replyId,
                Author = new AuthorDto()
                {
                    UserName = ValidUsername,
                    Email = ValidEmail
                },
                Content = content,
                CreationDate = creationDate,
                ModificationDate = modificationDate,
                ThreadId = ValidThreadId,
                Likes = 0,
                Dislikes = 0
            };
        }
        public static ReplyReadDto GetTestUpVotedReplyReadDto()
        {
            var replyReadDto = GetTestReplyReadDtoList()[0];
            replyReadDto.Likes += 1;
            return replyReadDto;
        }
        public static ReplyReadDto GetTestDownVotedReplyReadDto()
        {
            var replyReadDto = GetTestReplyReadDtoList()[0];
            replyReadDto.Dislikes += 1;
            return replyReadDto;
        }
        public static ReplyCreateDto GetTestReplyCreateDto()
        {
            return new ReplyCreateDto()
            {
                Content = GetTestString(ContentMinLength),
                ThreadId = ValidThreadId
            };
        }
        public static List<ReplyReadDto> GetTestReplyReadDtoList()
        {
            List<DateTime> creationDates = GetValidDates(ValidCreationDates);
            List<DateTime> modificationDates = GetValidDates(ValidModificationDates);

            return new List<ReplyReadDto>
            {
                GetTestReplyReadDto(1, creationDates[0], modificationDates[0], GetTestString(ContentMinLength,'a')),
                GetTestReplyReadDto(2, creationDates[1], modificationDates[1], GetTestString(ContentMinLength,'b')),
                GetTestReplyReadDto(3, creationDates[2], modificationDates[2], GetTestString(ContentMinLength,'c'))
            };
        }
        public static ReplyVote GetTestVote(VoteType voteType)
        {
            return new ReplyVote()
            {
                Id = DefaultId,
                ReplyId = DefaultReplyId,
                VoterUsername = ValidUsername,
                VoteType = voteType
            };
        }
        public static Mock<IRepliesRepository> GetTestRepliesRepository()
        {
            var mockRepository = new Mock<IRepliesRepository>();

            mockRepository.Setup(repository => repository.Create(It.IsAny<Reply>()))
                .Returns(GetTestReply());
            mockRepository.Setup(repository => repository.GetById(It.IsAny<int>()))
                .Returns(GetTestReply());
            mockRepository.Setup(repository => repository.FilterBy(It.IsAny<ReplyQueryParameters>()))
                .Returns(GetTestReplyList());
            mockRepository.Setup(repository => repository.Update(It.IsAny<int>(), It.IsAny<Reply>()))
                .Returns(GetTestReply())
                .Verifiable();
            mockRepository.Setup(repository => repository.Delete(It.IsAny<int>()))
                .Returns(GetTestReply());
            mockRepository.Setup(repository => repository.UpVote(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(GetTestUpVotedReply());
            mockRepository.Setup(repository => repository.DownVote(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(GetTestDownVotedReply());
            mockRepository.Setup(repository => repository.ChangeVote(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(GetTestReply());

            return mockRepository;
        }
        public static Mock<IReplyMapper> GetTestReplyMapper()
        {
            var mockMapper = new Mock<IReplyMapper>();

            mockMapper.Setup(mapper => mapper.Map(It.IsAny<ReplyCreateDto>(), It.IsAny<User>()))
                .Returns(GetTestReply());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<Reply>()))
                .Returns(GetTestReplyReadDto());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<List<Reply>>()))
                .Returns(GetTestReplyReadDtoList());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<Reply>(), It.IsAny<ReplyUpdateDto>()))
                .Returns(GetTestReply());

            return mockMapper;
        }
        public static Mock<IReplyService> GetTestReplyService()
        {
            var loggedUserId = DefaultId;
            var replyCreationTime = GetValidDates(ValidCreationDates)[0];
            var replyModificationTime = GetValidDates(ValidModificationDates)[0];
            var replyContent = GetTestString(ContentMinLength);

            var replyReadDto = GetTestReplyReadDto(DefaultReplyId, replyCreationTime, replyModificationTime, replyContent);
            var replyCreatDto = new ReplyCreateDto()
            {
                Content = replyContent,
                ThreadId = ValidThreadId
            };
            var replyUpdateDto = new ReplyUpdateDto()
            {
                Content = replyContent
            };
            var replyReadDtoList = GetTestReplyReadDtoList();
            var votesDto = new VotesDto()
            {
                Likes = new List<string>(),
                Dislikes = new List<string>()
            };

            var mockService = new Mock<IReplyService>();

            //Create
            mockService.Setup(service => service.Create(replyCreatDto, loggedUserId)).Returns(replyReadDto);
            //FilterBy
            mockService.Setup(service => service.FilterBy(It.IsAny<ReplyQueryParameters>())).Returns(replyReadDtoList);
            //GetById
            mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns(replyReadDto);
            //GetByThreadId
            mockService.Setup(service => service.GetByThreadId(It.IsAny<int>())).Returns(replyReadDtoList);
            //Update
            mockService.Setup(service => service.Update(It.IsAny<int>(), replyUpdateDto, loggedUserId)).Returns(replyReadDto);
            //Delete
            mockService.Setup(service => service.Delete(DefaultReplyId, loggedUserId)).Returns(replyReadDto);
            //UpVote
            mockService.Setup(service => service.UpVote(DefaultReplyId, loggedUserId)).Returns(replyReadDto);
            //DownVote
            mockService.Setup(service => service.DownVote(DefaultReplyId, loggedUserId)).Returns(replyReadDto);
            //GetReplyVotes
            mockService.Setup(service => service.GetReplyVotes(DefaultReplyId)).Returns(votesDto);

            return mockService;
        }
        #endregion Replies

        #region Threads
        public static Models.Thread GetTestThread()
        {
            id++;
            return new Models.Thread
            {
                Id = id,
                Title = ValidThreadTitle + id,
                Content = ValidThreadContent + id,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                AuthorId = id,
                Author = GetUser(),
                IsDeleted = false
            };
        }
        public static Models.Thread GetTestDefaultThread()
        {
            id++;
            return new Models.Thread
            {
                Id = DefaultId,
                Title = ValidThreadTitle,
                Content = ValidThreadContent,
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

        public static ShortThreadResponseDto GetTestThreadResponseDto()
        {
            return new ShortThreadResponseDto
            {
                Title = ValidThreadTitle,
                Content = ValidThreadContent,
                Likes = 0,
                Dislikes = 0,
                CreationDate = DateTime.Now
                //ToDo Add list<ReplyReadDto> when test model for Reply is ready 
            };
        }

        public static List<ShortThreadResponseDto> GetTestListOfThreadResponseDto(int count)
        {
            var listOfThreadResponseDto = new List<ShortThreadResponseDto>();
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
                .Returns(GetTestDefaultThread());
            mockRepository.Setup(repository => repository.GetAll())
                .Returns(GetTestThreads(3));
            mockRepository.Setup(repository => repository.GetAllByUserId(It.IsAny<int>()))
                .Returns(GetTestThreads(3));
            mockRepository.Setup(repository => repository.Details(It.IsAny<int>()))
                .Returns(GetTestDefaultThread());
            mockRepository.Setup(repository => repository.Update(It.IsAny<Models.Thread>(), It.IsAny<Models.Thread>()))
                .Returns(GetTestDefaultThread());
            //ToDo  mockRepository.Setup(repository => repository.FilterBy(It.IsAny<Thread>(), It.IsAny<ThreadQueryParameters>()))
            //  .Returns(GetTestThreadList(3));           

            return mockRepository;
        }

        public static Mock<IThreadMapper> GetTestThreadMapper()
        {
            var mockMapper = new Mock<IThreadMapper>();

            mockMapper.Setup(mapper => mapper.Map(It.IsAny<ThreadCreateDto>(), It.IsAny<User>()))
                .Returns(GetTestDefaultThread());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<Models.Thread>()))
                .Returns(GetTestThreadResponseDto());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<Models.Thread>(), It.IsAny<ThreadUpdateDto>()))
                .Returns(GetTestDefaultThread());
            mockMapper.Setup(mapper => mapper.Map(It.IsAny<List<Models.Thread>>()))
                .Returns(GetTestListOfThreadResponseDto(3));
            

            return mockMapper;
        }

        public static Mock<IThreadService> GetTestThreadServices()
        {
            var mockServices = new Mock<IThreadService>();

            mockServices.Setup(services => services.Create(It.IsAny<ThreadCreateDto>(), It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.Update(It.IsAny<int>(), It.IsAny<ThreadUpdateDto>(), It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());
            mockServices.Setup(services => services.GetAll())
                .Returns(GetTestListOfThreadResponseDto(0));
            mockServices.Setup(services => services.Details(It.IsAny<int>()))
                .Returns(GetTestThreadResponseDto());

            return mockServices;
        }
        #endregion Threads
    }
}
