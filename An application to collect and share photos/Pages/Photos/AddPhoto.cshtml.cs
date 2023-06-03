using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationLayer.AppServices;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Services;
using An_application_to_collect_and_share_photos.ViewModels;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace An_application_to_collect_and_share_photos.Pages
{
    [BindProperties]
    public class AddPhotoModel : PageModel
    {

        private readonly IPhotoService _photoService;
        public AddPhotoModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }


        [Display(Name = "Photo URL")]
        [Required(ErrorMessage = "Please enter a picture URL")]
        public string Url { get; set; }
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

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Якщо модель недійсна, поверніть сторінку з помилками валідації
                return Page();
            }

            var photo = new Photo
            {
                Id = ObjectId.GenerateNewId(),
                UserId = ObjectId.GenerateNewId(),
                AlbumId = null,
                Url = Url,
                Title = Title,
                Description = Description,
                CameraName = CameraName,
                Status = Status,
                UploadDate = DateTime.Now
                // Копіювати інші властивості з PhotoVM до Photo
            };

            // Використовуйте _photoService для збереження фото
            await _photoService.CreatePhotoAsync(photo);

            // Перенаправте користувача на іншу сторінку або повідомте про успішне додавання фото
            return RedirectToPage("/Index");
        }
    }
}
