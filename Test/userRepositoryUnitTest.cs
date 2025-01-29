using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace Test
{
    public class userRepositoryUnitTest
    {
        [Fact]
        public async Task LoginUser_ValidCredentials_ReturnUser()
        {
            var user = new User { FirstName = "aaa", LastName = "bbb", Email = "chani@gmail", Password = "Chani$1234"};
            var mockContext = new Mock<MyShopContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.loginUser(user.Email, user.Password);

            Assert.Equal(user, result); 
        }
    }
}