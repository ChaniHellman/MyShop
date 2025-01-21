using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using Repositories;
using Zxcvbn;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> getUserById(int id)
        {
            return await _userRepository.getUserById(id);

        }
        public async Task<User> AddUser(User user)
        {
            int PasswordStrength = checkPasswordStrength(user.Password);
            if (PasswordStrength > 2)
                return await _userRepository.addUser(user);
            else
                throw new Exception(PasswordStrength.ToString());
        }
        public async Task<User> loginUser(string email, string password)
        {
            return await _userRepository.loginUser(email, password);
        }
        public Task updateUser(int id, User userToUpdate)
        {
            return _userRepository.updateUser(id, userToUpdate);
        }
        public int checkPasswordStrength(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }



    }
}
