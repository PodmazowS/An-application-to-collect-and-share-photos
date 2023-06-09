using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class IndexModel : PageModel
    {
        
        private readonly ILogger<IndexModel> _logger;
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;
        public IndexModel(ILogger<IndexModel> logger, IPhotoService photoService, UserManager<User> userManager)
        {
            _photoService = photoService;
            _userManager = userManager;
        }

        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<User> Users { get; set; }

        public IActionResult OnGet()
        {
            Photos = _photoService.GetAll().Reverse();
            Users = _userManager.Users.ToList();

            return Page();
        }
    }
}