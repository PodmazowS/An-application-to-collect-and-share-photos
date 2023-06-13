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



        public Task<Album> GetAlbumByIdAsync(ObjectId albumId)
        {
            return _albums
                .Find(Builders<Album>.Filter.Eq(a => a.Id, albumId))
                .Project(a =>
                    new Album(
                        a.Id,
                        a.Title,
                        a.Status,
                        a.UserId,
                        a.Description
                    )
                ).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Album>> GetAlbumsByUserIdAsync(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAlbumAsync(Album album)
        {
            try
            {
                await _albums.InsertOneAsync(album);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the photo: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAlbumAsync(ObjectId id, Album album)
        {
            try
            {
                var filter = Builders<Album>.Filter.Eq(a => a.Id, id);
                var update = Builders<Album>.Update
                    .Set(a => a.Title, album.Title)
                    .Set(a => a.Status, album.Status)
                    .Set(a => a.UserId, album.UserId)
                    .Set(a => a.Description, album.Description);

                await _albums.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the album with ID {id}: {ex.Message}");
                throw;
            }
        }

        public Task DeleteAlbumAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}