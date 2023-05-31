using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Infrastructure.Data
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IMongoCollection<Album> _albumCollection;
        public AlbumRepository(IMongoDatabase database)
        {
            _albumCollection = database.GetCollection<Album>("albums"); 
        }




        //GetAll
        public async Task<IEnumerable<Album>> GetAll()
        {
            var albums = await _albumCollection.Find(_ => true).ToListAsync();
            return albums;

        }

        public async Task<Album> GetById(ObjectId id)
        {
            var filter = Builders<Album>.Filter.Eq("_id", id);
            var album = await _albumCollection.Find(filter).FirstOrDefaultAsync();
            return album;
        }



        public async Task Create(Album album)
        {
            await _albumCollection.InsertOneAsync(album);
        }

        public async Task Delete(ObjectId id)
        {
            var filter = Builders<Album>.Filter.Eq(a => a.Id, id);
            await _albumCollection.DeleteOneAsync(filter);
        }

        public async Task<Album> Update(ObjectId id, Album album)
        {
            var filter = Builders<Album>.Filter.Eq("_id", id);
            var options = new ReplaceOptions { IsUpsert = false };
            await _albumCollection.ReplaceOneAsync(filter, album, options);

            return album;
        }
    }
}
