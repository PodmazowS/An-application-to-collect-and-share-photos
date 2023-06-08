using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            UserPhotos = await _photoService.GetPhotosByUserIdAsync(user.Id);

            return Page();
        }
    }
}
