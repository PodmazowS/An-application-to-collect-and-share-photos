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
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(UserRole userRole);
        void DeleteUserRole(ObjectId userRoleId);
    }
}
