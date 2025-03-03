using Entities;
using Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly IUserRepository _userRepository;
        public  MyShopContext _context;

        public UserRepositoryIntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task AddUser_ValidUser_ShouldSaveToDatabase()
        {

            var user = new User { FirstName = "John", LastName = "Doe", Email = "ll@exam", Password = "Pass123!" };

            var savedUser = await _userRepository.addUser(user);

            Assert.NotNull(savedUser);
            Assert.NotEqual(0, savedUser.UserId);
            Assert.Equal("ll@exam", savedUser.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task LoginUser_ValidCredentials_ReturnUser()
        {
            var user = new User { FirstName = "Jane", LastName = "Doe", Email = "jane@example", Password = "JanePass!12" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepository.loginUser(user.Email, user.Password);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task LoginUser_InvalidCredentials_ReturnNull()
        {
            var result = await _userRepository.loginUser("invalid@ex", "WrongPass123");
            Assert.Null(result);
            _context.Dispose();

        }

        [Fact]
        public async Task GetUserById_ExistingUser_ReturnsUser()
        {
            var user = new User { FirstName = "Alice", LastName = "Smith", Email = "alice@ex", Password = "Alice1234!" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var foundUser = await _userRepository.getUserById(user.UserId);

            Assert.NotNull(foundUser);
            Assert.Equal(user.Email, foundUser.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task GetUserById_NonExistingUser_ReturnsNull()
        {
            var result = await _userRepository.getUserById(9999); 
            Assert.Null(result);
            _context.Dispose();

        }

        [Fact]
        public async Task UpdateUser_ValidUser_ShouldUpdateSuccessfully()
        {
            var user = new User { FirstName = "Tom", LastName = "Hanks", Email = "tom@ex", Password = "TomPass12!" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var updatedUser = new User { FirstName = "Tommy", LastName = "Hanks", Email = "tommy@ex", Password = "NewPass@34" };
            await _userRepository.updateUser(user.UserId, updatedUser);

            var result = await _userRepository.getUserById(user.UserId);

            Assert.NotNull(result);
            Assert.Equal("Tommy", result.FirstName);
            Assert.Equal("tommy@ex", result.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task UpdateUser_NonExistingUser_ShouldNotThrowException()
        {
            var nonExistingUser = new User { FirstName = "Ghost", LastName = "User", Email = "ghost@ex", Password = "Ghost123!" };

           await _userRepository.updateUser(9999, nonExistingUser);

            var result = await _userRepository.getUserById(nonExistingUser.UserId);
            Assert.Null(result);
            _context.Dispose();



        }
    }
}
