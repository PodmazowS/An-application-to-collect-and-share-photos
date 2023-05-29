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
    public class PhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<Photo> GetPhotoByIdAsync(ObjectId photoId)
        {
            try
            {
                var photo = await _photoRepository.GetPhotoByIdAsync(photoId);

                if (photo == null)
                {
                    throw new Exception($"Photo with id {photoId} not found.");
                }

                return photo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving photo with id {photoId}: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Photo>> GetPhotosByUserIdAsync(ObjectId userId)
        {
            try
            {
                var photo = await _photoRepository.GetPhotosByUserIdAsync(userId);

                if (photo == null)
                {
                    throw new Exception($"Photo with user id {userId} not found.");
                }

                return photo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving photo with user id {userId}: {ex.Message}");
                throw;
            }
        }

        public async Task CreatePhotoAsync(Photo photo)
        {
            try
            {
                var existingPhoto = await _photoRepository.GetPhotoByIdAsync(photo.Id);

                if (existingPhoto != null)
                {
                    throw new Exception($"Photo with id {photo.Id} already exists.");
                }

                await _photoRepository.CreatePhotoAsync(photo);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while creating the photo: {ex.Message}");
                throw; 
            }
        }

        public async Task UpdatePhotoAsync(Photo photo)
        {
            try
            {
                var existingPhoto = await _photoRepository.GetPhotoByIdAsync(photo.Id);

                if (existingPhoto == null)
                {
                    throw new Exception($"Photo with id {photo.Id} doesn't exist.");
                }

                await _photoRepository.UpdatePhotoAsync(photo);
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the photo: {ex.Message}");
                throw;
            }
        }

        public async Task DeletePhotoAsync(ObjectId photoId)
        {
            try
            {
                var existingPhoto = await _photoRepository.GetPhotoByIdAsync(photoId);

                if (existingPhoto == null)
                {
                    throw new Exception($"Photo with id {photoId} doesn't exist.");
                }

                await _photoRepository.DeletePhotoAsync(photoId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the photo: {ex.Message}");
                throw;
            }
        }
    }
}
