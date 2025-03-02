using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Services;


namespace Test
{
    public class UserRepositoryUnitTest
    {
        [Fact]
        public async Task LoginUser_ValidCredentials_ReturnsUser()
        {
            var user = new User { FirstName = "aaa", LastName = "bbb", Email = "chani@gmail.com", Password = "Chani$1234" };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User> { user });

            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.loginUser(user.Email, user.Password);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginUser_InvalidEmail_ReturnsNull()
        {
            var user = new User { FirstName = "aaa", LastName = "bbb", Email = "chani@gmail.com", Password = "Chani$1234" };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User> { user });

            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.loginUser("wrong@gmail.com", user.Password);

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginUser_InvalidPassword_ReturnsNull()
        {
            var user = new User { FirstName = "aaa", LastName = "bbb", Email = "chani@gmail.com", Password = "Chani$1234" };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User> { user });

            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.loginUser(user.Email, "WrongPass123");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_ExistingId_ReturnsUser()
        {
            var user = new User { UserId = 1, FirstName = "aaa", LastName = "bbb" };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User> { user });

            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.getUserById(1);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetUserById_NonExistingId_ReturnsNull()
        {
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());

            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.getUserById(99);

            Assert.Null(result);
        }
        [Fact]
        public async Task AddUser_ValidUser_ReturnsUser()
        {
            var user = new User { FirstName = "aaa", LastName = "bbb", Email = "h@gmail.com", Password = "Chani$851" };
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            var userRepository = new UserRepository(mockContext.Object);
            var result = await userRepository.addUser(user);
            Assert.Equal(user, result);
            mockContext.Verify(x => x.Users.AddAsync(user, default), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ExistingUser_UpdatesUser()
        {
            var user = new User { UserId = 1, FirstName = "aaa", LastName = "bbb" };
            var mockContext = new Mock<MyShopContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>() { user});
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            mockContext.Setup(x => x.Users.FindAsync(1)).ReturnsAsync(user);

            var userRepository = new UserRepository(mockContext.Object);
            var updatedUser = new User { FirstName = "updated", LastName = "user" };

            await userRepository.updateUser(1, updatedUser);

            Assert.Equal("updated", user.FirstName);  
            Assert.Equal("user", user.LastName); 
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
