using ApplicationLayer.DTO;
using Domain.Models;
using Domain.Services;
using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Infrastructure.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PhotoControllerMongoDb : ControllerBase
    {
        private readonly PhotoServiceMongoDb _service;
        
        public PhotoControllerMongoDb(PhotoServiceMongoDb service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<PhotoDto> FindAll()
        {
            return _service.GetAll().Select(PhotoDto.of).AsEnumerable();
        }
        
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> FindById(ObjectId id)
        {
            var photo = await _service.GetPhotoByIdAsync(id);
    
            if (photo is null)
            {
                return NotFound();
            }
    
            var result = PhotoDto.of(photo);
            return Ok(result);
        }
        
        
        [HttpGet]
        [Route("userId")]
        public async Task<IActionResult> FindByUserId(ObjectId userId)
        {
            var photos = await _service.GetPhotosByUserIdAsync(userId);
    
            if (photos == null || !photos.Any())
            {
                return NotFound();
            }
    
            return Ok(photos);
        }


        
        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhotoAsync(string photoId)
        {
            try
            {
                ObjectId objectId;

                if (!ObjectId.TryParse(photoId, out objectId))
                {
                    return BadRequest("Invalid photo ID format.");
                }

                await _service.DeletePhotoAsync(objectId);

                return Ok($"Photo with ID {photoId} has been deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the photo: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the photo.");
            }
        }
        [HttpPut("{photoId}")]
        public async Task<IActionResult> UpdatePhoto(string photoId, [FromBody] PhotoDto photoDto)
        {
            try
            {
                if (!ObjectId.TryParse(photoId, out ObjectId objectId))
                {
                    return BadRequest("Invalid photo ID format.");
                }

                // Create a new photo object with the updated properties
                var updatedPhoto = new Photo
                {
                    Id = objectId,
                    Url = photoDto.Url,
                    Title = photoDto.Title,
                    Description = photoDto.Description,
                    CameraName = photoDto.CameraName,
                    Status = photoDto.Status,
                    UploadDate = photoDto.UploadDate,
                    Tag = photoDto.Tag,
                    UserId = objectId
                };

                // Call the update method in the service
                await _service.UpdatePhotoAsync(updatedPhoto);

                return Ok($"Photo with ID {photoId} has been updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the photo: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the photo.");
            }
        }
        
    }
}
