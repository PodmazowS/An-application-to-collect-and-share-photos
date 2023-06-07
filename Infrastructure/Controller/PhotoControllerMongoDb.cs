using Domain.Models;
using Domain.Services;
using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;

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
    }
}
