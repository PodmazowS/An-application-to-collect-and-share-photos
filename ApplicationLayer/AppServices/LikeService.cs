using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using Domain.Services;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.AppServices;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;

    public LikeService(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task LikePhotoAsync(ObjectId userId, ObjectId photoId, Like like)
    {
        // Check if the user has already liked the photo
        bool hasLiked = await _likeRepository.HasUserLikedPhotoAsync(userId, photoId);
        if (hasLiked)
        {

            await _likeRepository.DeleteLikeAsync(userId, photoId);
        }

        // Create a new Like object
        //var like = new Like
        //{
        //    UserId = userId,
        //    PhotoId = photoId,
        //    LikeCount = 1,

        //};

        // Save the like to the repository
        await _likeRepository.CreateLikeAsync(like);
    }

    public async Task UnlikePhotoAsync(ObjectId userId, ObjectId photoId)
    {
        // Check if the user has liked the photo
        bool hasLiked = await _likeRepository.HasUserLikedPhotoAsync(userId, photoId);
        if (!hasLiked)
        {
            // Handle the case where the user has not liked the photo
            await _likeRepository.DeleteLikeAsync(userId, photoId);
        }

        // Remove the like from the repository
        await _likeRepository.DeleteLikeAsync(userId, photoId);
    }

    public async Task<int> GetLikeCountForPhotoAsync(ObjectId photoId)
    {
        // Get the like count for the photo from the repository
        return await _likeRepository.GetLikeCountForPhotoAsync(photoId);
    }
}
