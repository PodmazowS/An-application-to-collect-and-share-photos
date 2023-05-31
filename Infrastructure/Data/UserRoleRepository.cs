using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data;

public class UserRoleRepository
{
    private readonly IMongoCollection<UserRole> _userRoleCollection;

    public UserRoleRepository(IMongoDatabase database)
    {
        _userRoleCollection = database.GetCollection<UserRole>("userRoles");
    }
    public async Task<UserRole> GetUserRoleById(ObjectId roleId)
    {
        return await _userRoleCollection.Find(r => r.Id == roleId).FirstOrDefaultAsync();
    }

    public async Task<UserRole> GetUserRoleByName(string roleName)
    {
        return await _userRoleCollection.Find(r => r.RoleName == roleName).FirstOrDefaultAsync();
    }

    public async Task CreateUserRole(UserRole userRole)
    {
        await _userRoleCollection.InsertOneAsync(userRole);
    }

    public async Task UpdateUserRole(UserRole userRole)
    {
        await _userRoleCollection.ReplaceOneAsync(r => r.Id == userRole.Id, userRole);
    }

    public async Task DeleteUserRole(ObjectId roleId)
    {
        await _userRoleCollection.DeleteOneAsync(r => r.Id == roleId);
    }
}