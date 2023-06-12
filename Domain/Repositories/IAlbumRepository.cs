using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAlbumRepository
    {


        //Create
        Task Create(Album album);

        //GetAll
        IEnumerable<Album> GetAll();

        //GetById
        Task<Album> GetById(ObjectId id);

        //Update
        Task<Album> Update(ObjectId id, Album album);

        //Delete
        Task Delete(ObjectId id);
        //GetPhotos using UserId parameter
        Task<IEnumerable<Album>> GetAlbumsByUserIdAsync(ObjectId userId);

    }
}
