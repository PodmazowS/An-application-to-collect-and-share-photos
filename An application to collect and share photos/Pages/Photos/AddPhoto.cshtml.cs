using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationLayer.AppServices;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Services;
using An_application_to_collect_and_share_photos.ViewModels;
using MongoDB.Bson;

namespace An_application_to_collect_and_share_photos.Pages;

public class AddPhotoModel : PageModel
{
    private readonly IPhotoService _photoService;

    [BindProperty]
    public PhotoVM Photo { get; set; }

    public AddPhotoModel(IPhotoService photoService)
    {
        _photoService = photoService;
    }
    
    public void OnGet()
    {
    }
    [ValidateAntiForgeryToken]  
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Якщо модель недійсна, поверніть сторінку з помилками валідації
            return Page();
        }
        var photo = new Photo
        {
            Id = ObjectId.GenerateNewId(),
            UserId = ObjectId.GenerateNewId(),
            AlbumId = null,
            Url = Photo.Url,
            Title = Photo.Title,
            Description = Photo.Description,
            CameraName = Photo.CameraName,
            Status = Photo.Status,
            // Копіювати інші властивості з PhotoVM до Photo
        };
        // Використовуйте _photoService для збереження фото
        await _photoService.CreatePhotoAsync(photo);

        // Перенаправте користувача на іншу сторінку або повідомте про успішне додавання фото
        return RedirectToPage("/Index");
    }
}
