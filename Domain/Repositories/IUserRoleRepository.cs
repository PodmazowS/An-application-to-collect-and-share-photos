using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRole> GetUserRoleByIdAsync(ObjectId userRoleId);
        Task<UserRole> GetUserRoleByNameAsync(string roleName);
        Task CreateUserRoleAsync(UserRole userRole);
        Task UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(ObjectId userRoleId);
    }
}
