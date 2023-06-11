using ApplicationLayer.AppServices;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class ProfilePageModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;
        public ProfilePageModel(IPhotoService photoService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _photoService = photoService;
        }

        public IEnumerable<Photo> UserPhotos { get; set; }
        public User UserProfile { get; set; }

        public async Task<IActionResult> OnGetUserPage(string userId)
        {

            UserProfile = await _userManager.FindByIdAsync(userId);

            if (UserProfile == null)
            {
                return NotFound();
            }
            var reversedPhotos = await _photoService.GetPhotosByUserIdAsync(UserProfile.Id);

            UserPhotos = reversedPhotos.Reverse();

            return Page();
        }
        public async Task<IActionResult> OnGet()
        {

            UserProfile = await _userManager.GetUserAsync(User);
            var reversedPhotos = await _photoService.GetPhotosByUserIdAsync(UserProfile.Id);

            UserPhotos = reversedPhotos.Reverse();

            return Page();
        }
    }
}
