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
        User GetUserById(ObjectId userId);
        User GetUserByEmail(string email);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(ObjectId userId);
    }
}
