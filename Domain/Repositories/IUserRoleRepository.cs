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
        UserRole GetUserRoleById(ObjectId userRoleId);
        UserRole GetUserRoleByName(string roleName);
        Task CreateUserRole(UserRole userRole);
        Task UpdateUserRole(UserRole userRole);
        Task DeleteUserRole(ObjectId userRoleId);
    }
}
