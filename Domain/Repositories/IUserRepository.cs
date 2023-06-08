using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(ObjectId userId);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckPassword(ObjectId userId, string password);
        Task<IEnumerable<User>> GetAll();
        Task CreateUser(User user);
        Task<User> UpdateUser(ObjectId userId, User user);
        Task DeleteUser(ObjectId userId);
    }
}
