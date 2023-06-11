﻿using Domain.Models;
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
            try
            {
                var filter = Builders<Photo>.Filter.Eq(p => p.Id, photoId);
                return _photos.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the photo with ID {photoId}: {ex.Message}");
                throw;
                
            }
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
            return _photos
                .Find(Builders<Photo>.Filter.Eq(p => p.Id, photoId))
                .Project(p =>
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
                ).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Photo>> GetPhotosByAlbumIdAsync(ObjectId albumId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Photo>> GetPhotosByUserIdAsync(ObjectId userId)
        {
            var photos = await _photos
                .Find(Builders<Photo>.Filter.Eq(p => p.UserId, userId))
                .ToListAsync();

            return photos;
        }

        public Task UpdatePhotoAsync(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
