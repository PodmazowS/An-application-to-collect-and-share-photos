using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationLayer.AppServices;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Services;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace An_application_to_collect_and_share_photos.Pages
{
    [BindProperties]
    public class AddAlbumModel : PageModel
    {

        private readonly IAlbumService _albumService;
        public AddAlbumModel(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a title for the album")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter a description for an album")]
        public string Description { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select if you want the album to be public or private")]
        public string Status { get; set; }

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

            var album = new Album
            {
                Id = ObjectId.GenerateNewId(),
                UserId = ObjectId.GenerateNewId(),
                Title = Title,
                Description = Description,
                Status = Status,
            };

            // Використовуйте _albumService для збереження фото
            await _albumService.CreateAlbumAsync(album);

            // Перенаправте користувача на іншу сторінку або повідомте про успішне додавання фото
            return RedirectToPage("/Index");
        }
    }
}
