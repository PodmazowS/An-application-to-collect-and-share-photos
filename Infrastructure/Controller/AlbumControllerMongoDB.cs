using ApplicationLayer.DTO;
using Domain.Models;
using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Infrastructure.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AlbumControllerMongoDb : ControllerBase
    {
        private readonly AlbumServiceMongoDb _service;

        public AlbumControllerMongoDb(AlbumServiceMongoDb service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<AlbumDto> FindAll()
        {
            return _service.GetAllAlbums().Select(AlbumDto.of).AsEnumerable();
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> FindById(ObjectId id)
        {
            var album = await _service.GetAlbumByIdAsync(id);

            if (album is null)
            {
                return NotFound();
            }

            var result = AlbumDto.of(album);
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbumAsync(string id, [FromBody] AlbumDto albumDto)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    return BadRequest("Invalid album ID format.");
                }

                var album = new Album
                {
                    Id = objectId,
                    Title = albumDto.Title,
                    Status = albumDto.Status,
                    UserId = objectId,
                    Description = albumDto.Description
                };

                await _service.UpdateAlbumAsync(objectId, album);

                return Ok($"Album with ID {id} has been updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the album: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the album.");
            }
        }
        
        

    }
    
}