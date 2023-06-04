using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Numerics;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserRepository(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("user");
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            var user = await _userCollection.Find(_ => true).ToListAsync();
            return user;

        }

        public async Task<User> GetUserById(ObjectId userId)
        {
            var filter = Builders<User>.Filter.Eq("_id", userId);
            var user = await _userCollection.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            var user = await _userCollection.Find(filter).FirstOrDefaultAsync();
            return user;
        }
        public async Task<bool> CheckPassword(ObjectId userId, string password)
        {
            var user = await GetUserById(userId);
            if (user == null)
                return false;

            var result = Equals(user.PasswordHash, password);

            return result;
        }

        public async Task CreateUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        public async Task DeleteUser(ObjectId userId)
        {
            var filter = Builders<User>.Filter.Eq(a => a.Id, userId);
            await _userCollection.DeleteOneAsync(filter);
        }

        public async Task<User> UpdateUser(ObjectId userId, User user)
        {
            var filter = Builders<User>.Filter.Eq("_id", userId);
            var options = new ReplaceOptions { IsUpsert = false };
            await _userCollection.ReplaceOneAsync(filter, user, options);

            return user;
        }
    }
}
