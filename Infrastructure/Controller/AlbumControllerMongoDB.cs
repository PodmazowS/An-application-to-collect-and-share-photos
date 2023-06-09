using ApplicationLayer.DTO;
using Domain.Models;
using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;

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

    }
    
}