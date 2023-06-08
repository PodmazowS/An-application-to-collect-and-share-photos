using Domain.Models;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserRoleService
    {
        Task<UserRole> GetUserRoleByIdAsync(ObjectId roleId);
        Task<UserRole> GetUserRoleByNameAsync(string roleName);
        Task CreateUserRoleAsync(UserRole userRole);
        Task UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(ObjectId roleId);
    }
}
