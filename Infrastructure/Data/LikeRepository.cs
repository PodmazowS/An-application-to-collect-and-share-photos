using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;

namespace Infrastructure.Data
{
    public class LikeRepository : ILikeRepository
    {
        public int AddLikeToPost(Like like, ObjectId id)
        {
            throw new NotImplementedException();
        }

        public int DeleteLikeFromPost(Like like, ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
