using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Infrastructure.Controller
{
    [ApiController]
    [Route("api/[Controller]")]

    public class CommentControllerMongoDb : ControllerBase
    {
        private readonly CommentServiceMongoDb _service;

        public CommentControllerMongoDb(CommentServiceMongoDb service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("photoId")]

        public async Task<IActionResult> FindByPhotoId(ObjectId photoId)
        {
            var comments = await _service.GetCommentsForPhotoAsync(photoId);

            if (comments == null || !comments.Any())
            {
                return NotFound();
            }

            return Ok(comments);
        }
    }
}