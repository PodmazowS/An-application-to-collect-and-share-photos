using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using System.Numerics;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository

    {
        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
