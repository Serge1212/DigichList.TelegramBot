using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Digichlist.Tests.Repositories
{
    public class UserRepositoryTests
    {
        public readonly IUserRepository mockUserRepo;
        public UserRepositoryTests()
        {
            IReadOnlyList<User> users = new List<User>
            {
                new User{Id = 1, TelegramId = 55555, FirstName = "John",
                LastName = "williams", IsRegistered = true},
                new User{Id = 2, TelegramId = 44444, FirstName = "Matthew",
                LastName = "Paul", IsRegistered = true},
                new User{Id = 3, TelegramId = 33333, FirstName = "Drake",
                LastName = "Parker", IsRegistered = false},
            };

            Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();

            mockUserRepo.Setup(u => u.GetAllAsync().Result).Returns(users);

            mockUserRepo.Setup(u => u.GetByIdAsync(It.IsAny<int>()).Result)
                .Returns((int i) => users.Where(x => x.Id == i).FirstOrDefault());

            mockUserRepo.Setup(u => u.GetUserByTelegramIdAsync(It.IsAny<int>()).Result)
                .Returns((int telegramId) => users.Where(x => x.TelegramId == telegramId).FirstOrDefault());

            this.mockUserRepo = mockUserRepo.Object;
        }

        [Fact]
        public void CanReturnUserById()
        {
            User testUser = mockUserRepo.GetByIdAsync(2).Result;

            Assert.NotNull(testUser);
            Assert.IsAssignableFrom<User>(testUser);
            Assert.Equal("Matthew", testUser.FirstName);
        }

        [Fact]
        public void CanReturnAllUsers()
        {
            IReadOnlyList<User> testUsers = mockUserRepo.GetAllAsync().Result;

            Assert.NotNull(testUsers);
            Assert.Equal(3, testUsers.Count);
        }

        [Fact]
        public void CanReturnUserByTelegramId()
        {
            User testUser = mockUserRepo.GetUserByTelegramIdAsync(55555).Result;

            Assert.NotNull(testUser);
            Assert.IsAssignableFrom<User>(testUser);
            Assert.Equal("John", testUser.FirstName);
        }


        [Fact]
        public void GettingUserById()
        {
            var dbContextMock = new Mock<DigichListContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>()).Result).Returns(Task.FromResult(new User()).Result);
            dbContextMock.Setup(s => s.Set<User>()).Returns(dbSetMock.Object);

            var userRepo = new UserRepository(dbContextMock.Object);

            var user = userRepo.GetByIdAsync(15).Result;

            Assert.NotNull(user);
            Assert.IsAssignableFrom<User>(user);
        }
    }
}
