﻿using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetAll();
        Task<Photo> GetPhotoByIdAsync(ObjectId photoId);
        Task<IEnumerable<Photo>> GetPhotosByUserIdAsync(ObjectId userId);
        Task<IEnumerable<Photo>> GetPhotosByAlbumIdAsync(ObjectId albumId);
        Task CreatePhotoAsync(Photo photo);
        Task UpdatePhotoAsync(Photo photo);
        Task DeletePhotoAsync(ObjectId photoId);
    }
}
