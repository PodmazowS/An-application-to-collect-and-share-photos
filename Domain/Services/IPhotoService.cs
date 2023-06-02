using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPhotoService
    {
        public IEnumerable<Photo> GetAll();

		public Task<Photo> GetPhotoByIdAsync(ObjectId photoId);

        public Task<IEnumerable<Photo>> GetPhotosByUserIdAsync(ObjectId userId);

        public Task CreatePhotoAsync(Photo photo);

        public Task UpdatePhotoAsync(Photo photo);

        public Task DeletePhotoAsync(ObjectId photoId);
    }
}
