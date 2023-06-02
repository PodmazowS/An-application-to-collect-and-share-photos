using System.ComponentModel.DataAnnotations;

namespace An_application_to_collect_and_share_photos.ViewModels
{
    public class AlbumVM
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select a status")]
        public string Status { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
