using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IMongoCollection<Photo> _photoCollection;

        public PhotoRepository(IMongoDatabase database)
        {
            _photoCollection = database.GetCollection<Photo>("photos");
        }
        public async Task CreatePhotoAsync(Photo photo)
        {
            await _photoCollection.InsertOneAsync(photo);
        }
        public async Task<Photo> GetPhotoByIdAsync(ObjectId photoId)
        {
            var filter = Builders<Photo>.Filter.Eq("_id", photoId);
            var photo = await _photoCollection.Find(filter).FirstOrDefaultAsync();
            return photo;
        }
        public async Task<IEnumerable<Photo>> GetPhotosByUserIdAsync(ObjectId userId)
        {
            var filter = Builders<Photo>.Filter.Eq(p => p.UserId, userId);
            var photos = await _photoCollection.Find(filter).ToListAsync();
            return photos;
        }
        public async Task<IEnumerable<Photo>> GetPhotosByAlbumIdAsync(ObjectId albumId)
        {
            var filter = Builders<Photo>.Filter.Eq(p => p.AlbumId, albumId);
            var photos = await _photoCollection.Find(filter).ToListAsync();
            return photos;
        }

        public async Task UpdatePhotoAsync(Photo photo)
        {
            await _photoCollection.ReplaceOneAsync(p => p.Id == photo.Id, photo);
        }
        
        public async Task DeletePhotoAsync(ObjectId photoId)
        {
            await _photoCollection.DeleteOneAsync(p => p.Id == photoId);
        }

		public IEnumerable<Photo> GetAll()
		{
            return _photoCollection.Find(FilterDefinition<Photo>.Empty).ToList();
		}
	}
}
