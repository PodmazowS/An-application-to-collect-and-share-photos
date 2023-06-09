using ApplicationLayer.DTO;
using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.MongoDB
{
    public class AlbumServiceMongoDb : IAlbumService
    {
        private readonly IMongoCollection<Album> _albums;
        private readonly MongoClient _client;

        public AlbumServiceMongoDb(IOptions<MongoDBSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionUri);
            IMongoDatabase database = _client.GetDatabase(settings.Value.DatabaseName);
            _albums = database.GetCollection<Album>(settings.Value.AlbumCollection);
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            var albumMongoEntities = _albums.Find(Builders<Album>.Filter.Empty).ToList();
            var albums = albumMongoEntities.Select(a =>
                new Album(
                    a.Id,
                    a.Title,
                    a.Status,
                    a.UserId,
                    a.Description
                )
            );

            return albums;
        }



        public Task<Album> GetAlbumByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAlbumAsync(Album album)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAlbumAsync(ObjectId id, Album album)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAlbumAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}