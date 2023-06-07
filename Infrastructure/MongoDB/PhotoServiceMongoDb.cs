using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.MongoDB
{
    public class PhotoServiceMongoDb : IPhotoService
    {
        private readonly IMongoCollection<Photo> _photos;
        private readonly MongoClient _client;

        public PhotoServiceMongoDb(IOptions<MongoDBSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionUri);
            IMongoDatabase database = _client.GetDatabase(settings.Value.DatabaseName);
            _photos = database.GetCollection<Photo>(settings.Value.PhotoCollection);
        }

        public Task CreatePhotoAsync(Photo photo)
        {
            throw new NotImplementedException();
        }

        public Task DeletePhotoAsync(ObjectId photoId)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public IEnumerable<Photo> GetAll()
        {
            var photoMongoEntities = _photos.Find(Builders<Photo>.Filter.Empty).ToList();
            return _photos
                .Find(Builders<Photo>.Filter.Empty)
                .Project(
                    p =>
                        new Photo(
                            p.Id,
                            p.Url,
                            p.Title,
                            p.Description,
                            p.CameraName,
                            p.Status,
                            p.UploadDate,
                            p.Tag,
                            p.UserId
                            )
                ).ToEnumerable();
        }

        public Task<Photo> GetPhotoByIdAsync(ObjectId photoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Photo>> GetPhotosByUserIdAsync(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePhotoAsync(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
