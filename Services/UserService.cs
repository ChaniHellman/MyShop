using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using Repositories;
using Zxcvbn;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> getUserById(int id)
        {
            return await _userRepository.getUserById(id);

        }
        public async Task<User> AddUser(User user)
        {
            int PasswordStrength = checkPasswordStrength(user.Password);
            if (PasswordStrength > 2)
            {
                // Generate salt and hash password
                user.Salt = PasswordHelper.GenerateSalt();
                user.Password = PasswordHelper.HashPassword(user.Password, user.Salt);
                return await _userRepository.addUser(user);
            }
            else
                throw new Exception(PasswordStrength.ToString());
        }
        public async Task<User> loginUser(string email, string password)
        {
            // Get user by email
            var user = await _userRepository.getUserByEmail(email);
            if (user == null)
                return null;
            // Hash the input password with the stored salt
            var hashedInput = PasswordHelper.HashPassword(password, user.Salt);
            if (user.Password == hashedInput)
                return user;
            return null;
        }
        public Task updateUser(int id, User userToUpdate)
        {
            int PasswordStrength = checkPasswordStrength(userToUpdate.Password);
            if (PasswordStrength > 2) 
                return _userRepository.updateUser(id, userToUpdate);
            else
                throw new Exception(PasswordStrength.ToString());
        }
        public int checkPasswordStrength(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: null,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
