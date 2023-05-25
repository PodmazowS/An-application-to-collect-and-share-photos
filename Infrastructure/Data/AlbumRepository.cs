using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;

namespace Infrastructure.Data
{
    public class AlbumRepository : IAlbumRepository
    {
        //GetAll
        public async Task<IEnumerable<Album>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Album> GetById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Album album)
        {
            throw new NotImplementedException();
        }

        public Task Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<Album> Update(ObjectId id, Album album)
        {
            throw new NotImplementedException();
        }
    }
}
