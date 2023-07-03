using ForumSystemTeamFour.Data;
using ForumSystemTeamFour.Exceptions;
using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Repositories;
using ForumSystemTeamFour.Models.DTOs;
using ForumSystemTeamFour.Services;
using ForumSystemTeamFour.Tests.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ForumSystemTeamFour.Tests.TestData.TestModels;

namespace ForumSystemTeamFour.Tests
{

    [TestClass]
    public class UsersRepositoryTests 
    {        
        private static ForumDbContext TestContext;
        private static int NextTestId = 1;

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(databaseName: "ForumTestDB")
                .Options;

            TestContext = new ForumDbContext(dbContextOptions);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestContext.Database.EnsureDeleted();
        }
        

        [TestMethod]
        public void Create_ShouldCreateUser_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            int nextId = NextTestId++;
            var newUser = GetTestUser(nextId);

            var createdUser = testRepository.Create(newUser);
                        
            Assert.AreEqual(newUser, createdUser);
            
        }

        [TestMethod]
        public void Create_ShouldThrow_WhenUsernameIsDuplicate()
        {
            var testRepository = new UsersRepository(TestContext);            
            var firstUser = GetTestUser(NextTestId++);
            firstUser.Username = ValidUsername;
            
            var secondUser = GetTestUser(NextTestId++);
            secondUser.Username = ValidUsername;

            var createdUser = testRepository.Create(firstUser);

            Assert.ThrowsException<DuplicateEntityException>(() => testRepository.Create(secondUser));
        }

        [TestMethod]
        public void Create_ShouldThrow_WhenEmailIsDuplicate()
        {
            var testRepository = new UsersRepository(TestContext);
            var firstUser = GetTestUser(NextTestId++);
            firstUser.Email = ValidEmail;

            var secondUser = GetTestUser(NextTestId++);
            secondUser.Email = ValidEmail;

            var createdUser = testRepository.Create(firstUser);

            Assert.ThrowsException<DuplicateEntityException>(() => testRepository.Create(secondUser));
        }

        [TestMethod]
        public void Delete_ShouldDelete_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            var userToDelete = GetTestUser(NextTestId++);

            var deletedUser = testRepository.Delete(userToDelete);

            Assert.AreEqual(true, deletedUser.IsDeleted);
        }

        [TestMethod]
        public void FilterBy_ShouldReturnAll_WhenNoFilterIsProvided()
        {
            var testRepository = new UsersRepository(TestContext);
            var userOne = GetTestUser(NextTestId++);
            var userTwo = GetTestUser(NextTestId++);
            var userThree = GetTestUser(NextTestId++);
            
            var compareList = new List<User>() { userOne, userTwo, userThree };
            TestContext.AddRange(compareList);
            TestContext.SaveChanges();

            var filteredUsers = testRepository.FilterBy(userOne, new UserQueryParameters());

            Assert.AreEqual(true, filteredUsers.SequenceEqual(compareList));
        }

        [TestMethod]
        public void FilterBy_ShouldReturnFiltered_WhenFilteringByFirstName()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);            
            
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.FirstName = testUser.FirstName;

            var filteredUsers = testRepository.FilterBy(testUser, filterParameters);
            var userList = new List<User>() {testUser};

            Assert.AreEqual(true, filteredUsers.SequenceEqual(userList));
        }

        [TestMethod]
        public void FilterBy_ShouldReturnFiltered_WhenFilteringByLastName()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);

            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.LastName = testUser.LastName;

            var filteredUsers = testRepository.FilterBy(testUser, filterParameters);
            var userList = new List<User>() { testUser };

            Assert.AreEqual(true, filteredUsers.SequenceEqual(userList));
        }

        [TestMethod]
        public void FilterBy_ShouldReturnFiltered_WhenFilteringByUsername()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);

            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.Username = testUser.Username;

            var filteredUsers = testRepository.FilterBy(testUser, filterParameters);
            var userList = new List<User>() { testUser };

            Assert.AreEqual(true, filteredUsers.SequenceEqual(userList));
        }

        [TestMethod]
        public void FilterBy_ShouldThrow_WhenNonAdminFilteringByEmail()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);

            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.Email = testUser.Email;

            Assert.ThrowsException<UnauthorizedAccessException>(() => testRepository.FilterBy(testUser, filterParameters));
        }

        [TestMethod]
        public void FilterBy_ShouldReturnFiltered_WhenFilteringByEmail()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);
            testUser.IsAdmin = true;

            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.Email = testUser.Email;

            var filteredUsers = testRepository.FilterBy(testUser, filterParameters);
            var userList = new List<User>() { testUser };

            Assert.AreEqual(true, filteredUsers.SequenceEqual(userList));
        }

        [TestMethod]
        public void FilterBy_ShouldReturnFiltered_WhenFilteringByBlocked()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);
            testUser.IsBlocked = true;

            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.Blocked = true;

            var filteredUsers = testRepository.FilterBy(testUser, filterParameters);
            

            Assert.AreEqual(true, filteredUsers.All(user=>user.IsBlocked == true));
        }

        [TestMethod]
        public void FilterBy_ShouldReturnSorted_WhenSortingByFirstName()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUserOne = GetTestUser(NextTestId++);
            var testUserTwo = GetTestUser(NextTestId++);
            var testUserThree = GetTestUser(NextTestId++);
            testUserOne.FirstName = GetTestString(NamesMinLength, 'a');
            testUserTwo.FirstName = GetTestString(NamesMinLength, 'b');
            testUserThree.FirstName = GetTestString(NamesMinLength, 'c');

            TestContext.Add(testUserOne);
            TestContext.Add(testUserTwo);
            TestContext.Add(testUserThree);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.SortBy = "FirstName";

            var filteredUsers = testRepository.FilterBy(testUserOne, filterParameters);
            var firstFilteredUser = filteredUsers.First();

            Assert.AreEqual(testUserOne, firstFilteredUser);
        }

        [TestMethod]
        public void FilterBy_ShouldReturnSorted_WhenSortingByLastName()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUserOne = GetTestUser(NextTestId++);
            var testUserTwo = GetTestUser(NextTestId++);
            var testUserThree = GetTestUser(NextTestId++);
            testUserOne.LastName = GetTestString(NamesMinLength, 'a');
            testUserTwo.LastName = GetTestString(NamesMinLength, 'b');
            testUserThree.LastName = GetTestString(NamesMinLength, 'c');

            TestContext.Add(testUserOne);
            TestContext.Add(testUserTwo);
            TestContext.Add(testUserThree);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.SortBy = "LastName";

            var filteredUsers = testRepository.FilterBy(testUserOne, filterParameters);
            var firstFilteredUser = filteredUsers.First();

            Assert.AreEqual(testUserOne, firstFilteredUser);
        }

        [TestMethod]
        public void FilterBy_ShouldReturnSorted_WhenSortingByUsername()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUserOne = GetTestUser(NextTestId++);
            var testUserTwo = GetTestUser(NextTestId++);
            var testUserThree = GetTestUser(NextTestId++);
            testUserOne.Username = GetTestString(NamesMinLength, 'a');
            testUserTwo.Username = GetTestString(NamesMinLength, 'b');
            testUserThree.Username = GetTestString(NamesMinLength, 'c');

            TestContext.Add(testUserOne);
            TestContext.Add(testUserTwo);
            TestContext.Add(testUserThree);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.SortBy = "Username";

            var filteredUsers = testRepository.FilterBy(testUserOne, filterParameters);
            var firstFilteredUser = filteredUsers.First();

            Assert.AreEqual(testUserOne, firstFilteredUser);
        }

        [TestMethod]
        public void FilterBy_ShouldReturnOrdered_WhenSortingByDescending()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUserOne = GetTestUser(NextTestId++);
            var testUserTwo = GetTestUser(NextTestId++);
            var testUserThree = GetTestUser(NextTestId++);
            testUserOne.Username = GetTestString(NamesMaxLength, 'a');
            testUserTwo.Username = GetTestString(NamesMaxLength, 'b');
            testUserThree.Username = GetTestString(NamesMaxLength, 'z');

            TestContext.Add(testUserOne);
            TestContext.Add(testUserTwo);
            TestContext.Add(testUserThree);
            TestContext.SaveChanges();
            var filterParameters = new UserQueryParameters();
            filterParameters.SortBy = "Username";
            filterParameters.SortOrder = "Desc";

            var filteredUsers = testRepository.FilterBy(testUserOne, filterParameters);
            var firstFilteredUser = filteredUsers.First();

            Assert.AreEqual(testUserThree, firstFilteredUser);
        }

        [TestMethod]
        public void FilterBy_ShoulThrow_WhenNoUsersFound()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUserOne = GetTestUser(NextTestId++);
            var testUserTwo = GetTestUser(NextTestId++);
            var testUserThree = GetTestUser(NextTestId++);            

            TestContext.Add(testUserOne);
            TestContext.Add(testUserTwo);
            TestContext.Add(testUserThree);
            TestContext.SaveChanges();

            var filterParameters = new UserQueryParameters();
            filterParameters.Username = GetTestString(NamesMinLength, '!');

            Assert.ThrowsException<EntityNotFoundException>(() => testRepository.FilterBy(testUserOne, filterParameters));
        }

        [TestMethod]
        public void GetAll_ShoulReturn_AllUsers()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUserOne = GetTestUser(NextTestId++);
            var testUserTwo = GetTestUser(NextTestId++);
            var testUserThree = GetTestUser(NextTestId++);

            TestContext.Add(testUserOne);
            TestContext.Add(testUserTwo);
            TestContext.Add(testUserThree);
            TestContext.SaveChanges();

            var testSequence = new List<User>() {testUserOne, testUserTwo, testUserThree };
            var allUsers = testRepository.GetAll();

            Assert.AreEqual(true, allUsers.SequenceEqual(testSequence));
        }

        [TestMethod]
        public void GetByUsername_ShoulReturn_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);            

            TestContext.Add(testUser);            
            TestContext.SaveChanges();
            string searchUsername = testUser.Username;

            var foundUser = testRepository.GetByUsername(searchUsername);

            Assert.AreEqual(testUser, foundUser);
        }

        [TestMethod]
        public void GetByUsername_ShoulThroiw_WhenUserNotFound()
        {
            var testRepository = new UsersRepository(TestContext);
            var testUser = GetTestUser(NextTestId++);

            TestContext.Add(testUser);
            TestContext.SaveChanges();

            string searchUsername = GetTestString(UsernameMinLength, 'u');

            Assert.ThrowsException<EntityNotFoundException>(() => testRepository.GetByUsername(searchUsername));
        }

        [TestMethod]
        public void GetById_ShoulReturn_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            var searchId = NextTestId++;
            var testUser = GetTestUser(searchId);

            TestContext.Add(testUser);
            TestContext.SaveChanges();            

            var foundUser = testRepository.GetById(searchId);

            Assert.AreEqual(testUser, foundUser);
        }

        [TestMethod]
        public void GetById_ShoulThrow_WhenUserNotFound()
        {
            var testRepository = new UsersRepository(TestContext);
            var searchId = NextTestId+2;
            var testUser = GetTestUser(NextTestId++);

            TestContext.Add(testUser);
            TestContext.SaveChanges();

            Assert.ThrowsException<EntityNotFoundException>(() => testRepository.GetById(searchId));
        }

        [TestMethod]
        public void Update_ShoulUpdate_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            
            var testUser = GetTestUser(NextTestId++);
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var updateData = new UserUpdateDto();
            var updatedUser = testRepository.Update(testUser, updateData);            

            Assert.AreEqual(testUser, updatedUser);
        }

        [TestMethod]
        public void Update_ShoulThrow_WhenUpdatingUsernameOfNonAdmin()
        {
            var testRepository = new UsersRepository(TestContext);

            var testUser = GetTestUser(NextTestId++);
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var updateData = new UserUpdateDto();
            updateData.Username = GetTestString(UsernameMinLength, 'e');            

            Assert.ThrowsException<UnauthorizedAccessException>(() => testRepository.Update(testUser, updateData));
        }

        [TestMethod]
        public void Update_ShoulUpdateUsername_WhenAdmin()
        {
            var testRepository = new UsersRepository(TestContext);

            var testUser = GetTestUser(NextTestId++);
            testUser.IsAdmin = true;    
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var updateData = new UserUpdateDto();
            updateData.Username = GetTestString(UsernameMinLength, 'f');
            var updatedUser = testRepository.Update(testUser, updateData);

            Assert.AreEqual(testUser.Username, updatedUser.Username);
        }
        
        [TestMethod]
        public void Update_ShoulThrow_WhenUpdatingPhonenumberOfNonAdmin()
        {
            var testRepository = new UsersRepository(TestContext);

            var testUser = GetTestUser(NextTestId++);
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var updateData = new UserUpdateDto();
            updateData.PhoneNumber = ValidPhoneNumber; ;

            Assert.ThrowsException<UnauthorizedAccessException>(() => testRepository.Update(testUser, updateData));
        }

        [TestMethod]
        public void Update_ShoulUpdatePhonenumber_WhenAdmin()
        {
            var testRepository = new UsersRepository(TestContext);

            var testUser = GetTestUser(NextTestId++);
            testUser.IsAdmin = true;
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            var updateData = new UserUpdateDto();
            updateData.PhoneNumber = GetTestString(PhoneNumberMinLength, '1');
            var updatedUser = testRepository.Update(testUser, updateData);

            Assert.AreEqual(testUser.PhoneNumber, updatedUser.PhoneNumber);
        }

        [TestMethod]
        public void PromoteToAdmin_ShoulPhomore_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToPromote = NextTestId++;
            var testUser = GetTestUser(idToPromote);
            
            TestContext.Add(testUser);
            TestContext.SaveChanges();
            
            var promotedUser = testRepository.PromoteToAdmin(idToPromote);

            Assert.AreEqual(true, promotedUser.IsAdmin);
        }

        [TestMethod]
        public void PromoteToAdmin_ShoulThrow_WhenUserIsAlreadyAdmin()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToPromote = NextTestId++;
            var testUser = GetTestUser(idToPromote);
            testUser.IsAdmin = true;
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            Assert.ThrowsException<InvalidUserInputException>(() => testRepository.PromoteToAdmin(idToPromote));
        }

        [TestMethod]
        public void DemoteFromAdmin_ShoulDemote_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToDemote = NextTestId++;
            var testUser = GetTestUser(idToDemote);
            testUser.IsAdmin = true;
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            var promotedUser = testRepository.DemoteFromAdmin(idToDemote);

            Assert.AreEqual(false, promotedUser.IsAdmin);
        }

        [TestMethod]
        public void DemoteFromAdmin_ShoulThrow_WhenUserIsNotAdmin()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToDemote = NextTestId++;
            var testUser = GetTestUser(idToDemote);
            
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            Assert.ThrowsException<InvalidUserInputException>(() => testRepository.DemoteFromAdmin(idToDemote));
        }

        [TestMethod]
        public void Block_ShouldBlock_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToBlock = NextTestId++;
            var testUser = GetTestUser(idToBlock);
            
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            var promotedUser = testRepository.Block(idToBlock);

            Assert.AreEqual(true, promotedUser.IsBlocked);
        }

        [TestMethod]
        public void Block_ShoulThrow_WhenUserIsAlreadyBlocked()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToBlock = NextTestId++;
            var testUser = GetTestUser(idToBlock);
            testUser.IsBlocked = true;
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            Assert.ThrowsException<InvalidUserInputException>(() => testRepository.Block(idToBlock));
        }

        [TestMethod]
        public void Block_ShoulThrow_WhenUserIsAdmin()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToBlock = NextTestId++;
            var testUser = GetTestUser(idToBlock);
            testUser.IsAdmin = true;
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            Assert.ThrowsException<InvalidUserInputException>(() => testRepository.Block(idToBlock));
        }

        [TestMethod]
        public void Unblock_ShouldUnblock_WhenInputIsValid()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToUnblock = NextTestId++;
            var testUser = GetTestUser(idToUnblock);
            testUser.IsBlocked = true;
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            var promotedUser = testRepository.Unblock(idToUnblock);

            Assert.AreEqual(false, promotedUser.IsBlocked);
        }

        [TestMethod]
        public void Unblock_ShoulThrow_WhenUserIsNotBlocked()
        {
            var testRepository = new UsersRepository(TestContext);
            int idToDemote = NextTestId++;
            var testUser = GetTestUser(idToDemote);
            
            TestContext.Add(testUser);
            TestContext.SaveChanges();

            Assert.ThrowsException<InvalidUserInputException>(() => testRepository.Unblock(idToDemote));
        }
    }

}
