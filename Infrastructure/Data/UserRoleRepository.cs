using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{

    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IMongoCollection<UserRole> _userRoleCollection;

        public UserRoleRepository(IMongoDatabase database)
        {
            _userRoleCollection = database.GetCollection<UserRole>("userRoles");
        }
        public async Task<UserRole> GetUserRoleByIdAsync(ObjectId roleId)
        {
            return await _userRoleCollection.Find(r => r.Id == roleId).FirstOrDefaultAsync();
        }

        public async Task<UserRole> GetUserRoleByNameAsync(string roleName)
        {
            return await _userRoleCollection.Find(r => r.Name == roleName).FirstOrDefaultAsync();
        }

        public async Task CreateUserRoleAsync(UserRole userRole)
        {
            await _userRoleCollection.InsertOneAsync(userRole);
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            await _userRoleCollection.ReplaceOneAsync(r => r.Id == userRole.Id, userRole);
        }

        public async Task DeleteUserRoleAsync(ObjectId roleId)
        {
            await _userRoleCollection.DeleteOneAsync(r => r.Id == roleId);
        }
    }
}