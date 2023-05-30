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

        public Task<bool> HasUserLikedPhotoAsync(ObjectId userId, ObjectId photoId)
        {
            throw new NotImplementedException();
        }

        public Task CreateLikeAsync(Like like)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLikeAsync(ObjectId userId, ObjectId photoId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetLikeCountForPhotoAsync(ObjectId photoId)
        {
            throw new NotImplementedException();
        }
    }
}
