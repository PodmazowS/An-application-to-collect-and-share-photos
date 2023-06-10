using ApplicationLayer.AppServices;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace An_application_to_collect_and_share_photos.Pages.Albums
{
    public class ViewAlbumsPageModel : PageModel
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<User> _userManager;
        public ViewAlbumsPageModel(IAlbumService albumService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _albumService = albumService;
        }

        public IEnumerable<Album> UserAlbums { get; set; }
        public User UserProfile { get; set; }

        public User currentUser { get; set; }

        public async Task<IActionResult> OnGetUserAlbums(string userId)
        {
            UserProfile = await _userManager.FindByIdAsync(userId);

            if (UserProfile == null)
            {
                return NotFound();
            }
            currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null)
            {
                return NotFound();
            }
            var reversedAlbums = await _albumService.GetAlbumsByUserIdAsync(UserProfile.Id);

            UserAlbums = reversedAlbums.Reverse();

            return Page();
        }

        public void OnGet()
        {
        }
    }
}
