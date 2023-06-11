using ApplicationLayer.AppServices;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace An_application_to_collect_and_share_photos.Pages.Albums
{
    public class ViewAlbumModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;
        private readonly IAlbumService _albumService;
        public ViewAlbumModel(IPhotoService photoService, UserManager<User> userManager, IAlbumService albumService)
        {
            _userManager = userManager;
            _photoService = photoService;
            _albumService = albumService;
        }

        public IEnumerable<Photo> UserPhotos { get; set; }
        public User UserProfile { get; set; }
        public User currentUser { get; set; }
        public ObjectId AlbumId { get; set; }
        public async Task<IActionResult> OnGetUserPage(string userId, ObjectId albumId)
        {
            UserProfile = await _userManager.FindByIdAsync(userId);

            if (UserProfile == null)
            {
                return NotFound();
            }
            currentUser = await _userManager.GetUserAsync(User);

            AlbumId = albumId;
            var reversedPhotos = await _photoService.GetPhotosByAlbumIdAsync(albumId);

            UserPhotos = reversedPhotos.Reverse();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAlbum(ObjectId albumId, string userId)
        {
            UserProfile = await _userManager.FindByIdAsync(userId);

            if (UserProfile == null)
            {
                return NotFound();
            }
            currentUser = await _userManager.GetUserAsync(User);

            await _albumService.DeleteAlbumAsync(albumId);


            var photosToUpdate = await _photoService.GetPhotosByAlbumIdAsync(albumId);

            foreach (var photo in photosToUpdate)
            {
                photo.AlbumId = null;
                // Зберегти зміни до фотографії у базі даних
                await _photoService.UpdatePhotoAsync(photo);
            }

            return RedirectToPage("/Albums/DeleteSuccess");
        }

    }
}
