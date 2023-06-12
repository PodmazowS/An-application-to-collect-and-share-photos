using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace An_application_to_collect_and_share_photos.Pages.Photos
{
    [BindProperties]
    [Authorize(Roles = "Admin, User")]
    public class EditPhotoModel : PageModel
    {

        private readonly IPhotoService _photoService;
        private readonly ICommentService _commentService;
        private readonly ILikeService _likeService;

        public EditPhotoModel(IPhotoService photoService, ICommentService commentService, ILikeService likeService)
        {
            _photoService = photoService;
            _commentService = commentService;
            _likeService = likeService;
        }

        public static ObjectId PhotoId;

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a title for the image")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter a description for an image")]
        public string Description { get; set; }
        [Display(Name = "Camera Name")]
        [Required(ErrorMessage = "Please enter the camera name for your photo")]
        public string CameraName { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select if you want the photo to be public or private")]
        public string Status { get; set; }
        [Display(Name = "Tag")]
        [Required(ErrorMessage = "Please enter the photo tag")]
        public string Tag { get; set; }
        public void OnGet(ObjectId photoId)
        {
            PhotoId = photoId;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Якщо модель недійсна, поверніть сторінку з помилками валідації
                return Page();
            }

            var existingPhoto = await _photoService.GetPhotoByIdAsync(PhotoId);

            var photo = new Photo
            {
                Id = existingPhoto.Id,
                UserId = existingPhoto.UserId,
                AlbumId = existingPhoto.AlbumId,
                Url = existingPhoto.Url,
                Title = this.Title,
                Description = this.Description,
                CameraName = this.CameraName,
                Status = this.Status,
                UploadDate = existingPhoto.UploadDate,
                Tag = this.Tag
            };

            await _photoService.UpdatePhotoAsync(photo);

            return RedirectToPage("/Photos/EditSuccess");
        }

        public async Task<IActionResult> OnPostDeletePhoto()
        {
            await _photoService.DeletePhotoAsync(PhotoId);


            var comments = await _commentService.GetCommentsForPhotoAsync(PhotoId);

            foreach (var comment in comments)
            {
                await _commentService.DeleteCommentAsync(comment);
            }

            var likes = await _likeService.GetLikesByPhotoIdAsync(PhotoId);

            foreach(var like in likes)
            {
                await _likeService.UnlikePhotoAsync(like.UserId, like.PhotoId);
            }

            return RedirectToPage("/Photos/DeleteSuccess");
        }
    }
}
