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
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedFilter { get; set; }
        public List<Photo> Photos { get; set; }
        public List<User> Users { get; set; }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                string selectedFilter = Request.Query["filter"];

                var photos = _photoService.GetAll().Reverse();

                Photos = photos.Where(p => PropertyHasSimilarValue(p, selectedFilter, SearchQuery))
                    .ToList();

                Users = _userManager.Users.ToList();

                return Page();
            }

            Photos = _photoService.GetAll().Reverse().Take(5).ToList();
            Users = _userManager.Users.ToList();

            return Page();
        }
        private bool PropertyHasSimilarValue(Photo photo, string propertyName, string searchQuery)
        {
            // Отримати значення властивості фотографії за назвою propertyName
            var propertyValue = GetPropertyValue(photo, propertyName);

            // Перевірити, чи містить значення властивості схожу вартість з searchQuery
            return propertyValue != null && propertyValue.ToLower().Contains(searchQuery.ToLower());
        }

        private string GetPropertyValue(Photo photo, string propertyName)
        {
            // Використовуйте рефлексію, щоб отримати значення властивості за її назвою
            var property = photo.GetType().GetProperty(propertyName);
            var propertyValue = property?.GetValue(photo)?.ToString();

            return propertyValue;
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
