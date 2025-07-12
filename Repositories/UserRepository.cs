using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;




namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        MyShopContext _shopContext;
        public UserRepository(MyShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<User> getUserById(int id)
        {
            User user = await _shopContext.Users.Include(u => u.Orders).FirstOrDefaultAsync(e => e.UserId == id);
            return user;


        }

        public async Task<User?> addUser(User user)
        {

            bool emailExists = await _shopContext.Users.AnyAsync(u => u.Email == user.Email);
            if (emailExists)
            {
                return null;
            }

            await _shopContext.Users.AddAsync(user);
            await _shopContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> loginUser(string email, string password)
        {
            return await _shopContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
        }
        public async Task updateUser(int id, User newUser)
        {
            var existingUser = await _shopContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return; 
            }

            // עדכון השדות
            existingUser.FirstName = newUser.FirstName;
            existingUser.LastName = newUser.LastName;
            existingUser.Email = newUser.Email;
            existingUser.Password = newUser.Password;

            await _shopContext.SaveChangesAsync();
        }

        public async Task<User?> getUserByEmail(string email)
        {
            return await _shopContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }



    }
}
