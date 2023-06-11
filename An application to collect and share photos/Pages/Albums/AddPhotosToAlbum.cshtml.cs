using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace An_application_to_collect_and_share_photos.Pages.Albums
{
    public class AddPhotostoAlbumModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;

        private string _userId;
        private ObjectId _albumId;
        public AddPhotostoAlbumModel(IPhotoService photoService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _photoService = photoService;
        }
        public List<ObjectId> SelectedPhotos { get; set; }
        public IEnumerable<Photo> UserPhotos { get; set; }
        public User UserProfile { get; set; }
        public static ObjectId AlbumId { get; set; }

        public async Task<IActionResult> OnGet(string userId, ObjectId albumId)
        {
            _userId = userId;
            _albumId = albumId;

            UserProfile = await _userManager.FindByIdAsync(userId);

            if (UserProfile == null)
            {
                return NotFound();
            }

            AlbumId = albumId;

            var reversedPhotos = await _photoService.GetPhotosByUserIdAsync(UserProfile.Id);

            UserPhotos = reversedPhotos.Reverse();

            return Page();
        }
        public async Task<IActionResult> OnPostAddPhotos(string userId)
        {
            UserProfile = await _userManager.FindByIdAsync(userId);

            var reversedPhotos = await _photoService.GetPhotosByUserIdAsync(UserProfile.Id);

            UserPhotos = reversedPhotos.Reverse();
            // Отримати значення вибраних чекбоксів з форми
            SelectedPhotos = Request.Form["selectedPhotos"].Select(ObjectId.Parse).ToList();

            // Виконати необхідні дії з вибраними фотографіями
            foreach (var selectedItemId in SelectedPhotos)
            {
                // Знайти фотографію за її ідентифікатором та встановити значення атрибуту AlbumId
                var photo = await _photoService.GetPhotoByIdAsync(selectedItemId);
                if (photo != null)
                {
                    photo.AlbumId = AlbumId;
                    // Зберегти зміни до фотографії у базі даних
                    await _photoService.UpdatePhotoAsync(photo);
                }
            }

            // Перенаправити користувача на іншу сторінку після обробки форми
            return RedirectToPage("/Albums/AddPhotosToAlbumSuccess");

        }


    }
}
