using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class PhotoRepository : IPhotoRepository
    {

        public Task CreatePhotoAsync(Photo photo)
        {
            throw new NotImplementedException();
        }

        public Task DeletePhotoAsync(ObjectId photoId)
        {
            throw new NotImplementedException();
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
