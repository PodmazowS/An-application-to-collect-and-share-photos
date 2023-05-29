using Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILikeRepository
    {
        Task<bool> HasUserLikedPhotoAsync(ObjectId userId, ObjectId photoId);
        Task CreateLikeAsync(Like like);
        Task DeleteLikeAsync(ObjectId userId, ObjectId photoId);
        Task<int> GetLikeCountForPhotoAsync(ObjectId photoId);
    }
}
