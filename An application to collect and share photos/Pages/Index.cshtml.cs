using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace An_application_to_collect_and_share_photos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;

        public IndexModel( IPhotoService photoService, UserManager<User> userManager)
        {
            _photoService = photoService;
            _userManager = userManager;
        }

        public List<Photo> Photos { get; set; }
        public List<User> Users { get; set; }

        public IActionResult OnGet()
        {
            Photos = _photoService.GetAll().Reverse().Take(5).ToList();
            Users = _userManager.Users.ToList();

            return Page();
        }

        public IActionResult OnGetLoadMore(int itemsPerPage,int numberOfPage)
        { 
            Console.WriteLine($"{numberOfPage} {itemsPerPage}");
            var skip = (numberOfPage - 1) * itemsPerPage;
            var photos = _photoService.GetAll().Reverse().Skip(skip).Take(itemsPerPage).ToList();
            var users = _userManager.Users.ToList();

            return Partial("_PhotoItems", new IndexModel( _photoService, _userManager)
            {
                Photos = photos,
                Users = users
            });
        }
    }
}
