using ApplicationLayer.AppServices;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace An_application_to_collect_and_share_photos.Pages.Albums
{
    
    [Authorize(Roles = "Admin, User")]
    public class EditAlbumModel : PageModel
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;
        public EditAlbumModel(IAlbumService albumService, UserManager<User> userManager, IPhotoService photoService)
        {
            _photoService = photoService;
            _albumService = albumService;
            _userManager = userManager;
        }
        [BindProperty]
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a title for the album")]
        public string Title { get; set; }
        [BindProperty]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter a description for an album")]
        public string Description { get; set; }
        [BindProperty]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select if you want the album to be public or private")]
        public string Status { get; set; }

        public static ObjectId AlbumId;
        public User UserProfile { get; set; }
        public async Task<IActionResult> OnGet(ObjectId albumId, string userId)
        {
            UserProfile = await _userManager.FindByIdAsync(userId);
            if (UserProfile == null)
            {
                return NotFound();
            }
            AlbumId = albumId;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string userId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UserProfile = await _userManager.FindByIdAsync(userId);
            if (UserProfile == null)
            {
                return NotFound();
            }

            var existingAlbum = await _albumService.GetAlbumByIdAsync(AlbumId);

            var album = new Album
            {
                Id = existingAlbum.Id,
                UserId = existingAlbum.UserId,
                Title = this.Title,
                Description = this.Description,
                Status = this.Status
            };

            await _albumService.UpdateAlbumAsync(existingAlbum.Id,album);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAlbum()
        {

            await _albumService.DeleteAlbumAsync(AlbumId);


            var photosToUpdate = await _photoService.GetPhotosByAlbumIdAsync(AlbumId);

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
