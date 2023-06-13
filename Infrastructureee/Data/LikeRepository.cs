using Domain.Models;
using Domain.Repositories;
/*using Microsoft.Extensions.Hosting;*/
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly IMongoCollection<Like> _likeCollection;

        public LikeRepository(IMongoDatabase database)
        {
            _likeCollection = database.GetCollection<Like>("likes");
        }
        public int AddLikeToPost(Like like, ObjectId id)
        {
            like.PhotoId = id;
            _likeCollection.InsertOne(like);
            return 1; // Zwracamy 1 żeby pokazać że reakcja została pomyślnie dodana
        }

        public int DeleteLikeFromPost(Like like, ObjectId id)
        {
            var filter = Builders<Like>.Filter.Eq(x => x.UserId, like.UserId) & Builders<Like>.Filter.Eq(x => x.PhotoId, id);
            var result = _likeCollection.DeleteOne(filter);
            return (int)result.DeletedCount;
        }

        public async Task<bool> HasUserLikedPhotoAsync(ObjectId userId, ObjectId photoId)
        {
            var filter = Builders<Like>.Filter.Eq(x => x.UserId, userId) & Builders<Like>.Filter.Eq(x => x.PhotoId, photoId);
            var count = await _likeCollection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task CreateLikeAsync(Like like)
        {
            await _likeCollection.InsertOneAsync(like);
        }

        public async Task DeleteLikeAsync(ObjectId userId, ObjectId photoId)
        {
            var filter = Builders<Like>.Filter.Eq(x => x.UserId, userId) & Builders<Like>.Filter.Eq(x => x.PhotoId, photoId);
            await _likeCollection.DeleteOneAsync(filter);
        }

        public async Task<int> GetLikeCountForPhotoAsync(ObjectId photoId)
        {
            var filter = Builders<Like>.Filter.Eq("photoid", photoId);
            var count = await _likeCollection.CountDocumentsAsync(filter);
            return (int)count;
        }
        public async Task<List<Like>> GetLikesByPhotoIdAsync(ObjectId photoId)
        {
            var filter = Builders<Like>.Filter.Eq("photoid", photoId);
            var likes = await _likeCollection.Find(filter).ToListAsync();
            return likes;
        }

    }
}
