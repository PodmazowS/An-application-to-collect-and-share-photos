using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace Domain.Services;

public interface ILikeService
{
    public Task LikePhotoAsync(ObjectId userId, ObjectId photoId, Like like);

    public Task UnlikePhotoAsync(ObjectId userId, ObjectId photoId);

    public Task<int> GetLikeCountForPhotoAsync(ObjectId photoId);
    Task<List<Like>> GetLikesByPhotoIdAsync(ObjectId photoId);
}
