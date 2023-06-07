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
    }
}
