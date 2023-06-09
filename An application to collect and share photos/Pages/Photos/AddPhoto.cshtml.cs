using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationLayer.AppServices;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Services;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace An_application_to_collect_and_share_photos.Pages
{
    [BindProperties]
    [Authorize(Roles = "Admin, User")]
    public class AddPhotoModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;
        public AddPhotoModel(IPhotoService photoService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _photoService = photoService;
        }


        [Display(Name = "Photo URL")]
        [Required(ErrorMessage = "Please enter a picture URL")]
        public string PhotoUrl { get; set; }
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
            User user = await _userManager.GetUserAsync(User);

            var photo = new Photo
            {
                Id = ObjectId.GenerateNewId(),
                UserId = user.Id,
                AlbumId = null,
                Url = PhotoUrl,
                Title = Title,
                Description = Description,
                CameraName = CameraName,
                Status = Status,
                UploadDate = DateTime.Now,
                Tag = Tag
                // Копіювати інші властивості з PhotoVM до Photo
            };

            // Використовуйте _photoService для збереження фото
            await _photoService.CreatePhotoAsync(photo);


            // Перенаправте користувача на іншу сторінку або повідомте про успішне додавання фото
            return RedirectToPage("/Index");
        }
    }
}
