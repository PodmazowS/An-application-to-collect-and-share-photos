using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserByIdAsync(ObjectId userId);
        public Task<User> GetUserByEmailAsync(string email);
        public Task CreateUserAsync(User user);
        public Task DeleteUserAsync(ObjectId userId);
        public Task UpdateUserAsync(ObjectId userId, User user);
        Task<bool> CheckPasswordAsync(ObjectId userId, string password);


    }
}
