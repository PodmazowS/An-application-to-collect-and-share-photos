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
    public interface IAlbumService
    {
        public Task<IEnumerable<Album>> GetAllAlbums();

        public Task<Album> GetAlbumByIdAsync(ObjectId id);


        public Task CreateAlbumAsync(Album album);


        public Task UpdateAlbumAsync(ObjectId id, Album album);


        public Task DeleteAlbumAsync(ObjectId id);
    }
}
