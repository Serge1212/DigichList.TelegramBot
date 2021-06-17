using Digichlist.Tests.TestData;
using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
namespace Digichlist.Tests.Repositories
{
    public class IRepositoryTests
    {
        private readonly IRepository<User, int> _mockRepo;

        private readonly Mock<IRepository<User, int>> mockRepo = new Mock<IRepository<User, int>>();

        public IRepositoryTests()
        {
            List<User> users = UsersTestData.GetUsersTestData();

            mockRepo.Setup(u => u.GetAllAsync().Result).Returns(users);

            mockRepo.Setup(u => u.GetByIdAsync(It.IsAny<int>()).Result)
                .Returns((int i) => users.FirstOrDefault(x => x.Id == i));

            mockRepo.Setup(d => d.DeleteAsync(It.IsAny<User>()));

            mockRepo.Setup(u => u.UpdateAsync(It.IsAny<User>()));

            mockRepo.Setup(a => a.AddAsync(It.IsAny<User>()));

            mockRepo.Setup(u => u.GetAsync(It.IsAny<Expression<Func<User, bool>>>()).Result)
                .Returns((Expression<Func<User, bool>> predicate) 
                    => users.AsQueryable().Where(predicate).ToList());

            _mockRepo = mockRepo.Object;
        }

        [Fact]
        public async Task Delete()
        {
            var user = new User
            {
                Id = 22,
                FirstName = "Wannabedeleted",
                LastName = "ASAP",
                TelegramId = 123123123
            };

            await _mockRepo.DeleteAsync(user);

            mockRepo.Verify(r => r.DeleteAsync(user));
        }

        [Fact]
        public async Task SuccessfulUpdate()
        {
            var user = _mockRepo.GetByIdAsync(1).Result;

            user.LastName = "asdf";

            await _mockRepo.UpdateAsync(user);

            Assert.Equal("asdf", _mockRepo.GetByIdAsync(1).Result.LastName);
        }

        [Fact]
        public async Task CheckUsersByExpression()
        {
            var result = await _mockRepo.GetAsync(x => x.IsRegistered);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CheckAdding()
        {
            var user = new User
            {
                Id = 123,
                LastName = "asdf",
                TelegramId = 123123543
            };

            await _mockRepo.AddAsync(user);

            mockRepo.Verify(a => a.AddAsync(user));
        }
    }
}
