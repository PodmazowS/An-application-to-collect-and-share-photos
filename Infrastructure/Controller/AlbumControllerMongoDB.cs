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
        
        

    }
    
}