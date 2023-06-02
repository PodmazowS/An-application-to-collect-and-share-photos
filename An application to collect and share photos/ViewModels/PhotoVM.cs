using System.ComponentModel.DataAnnotations;

namespace An_application_to_collect_and_share_photos.ViewModels
{
    public class PhotoVM
    {
        [Display(Name = "Photo URL")]
		[Required(ErrorMessage = "Please enter a picture URL")]
		public string Url { get; set; }
		[Display(Name = "Title")]
		[Required(ErrorMessage = "Please enter a title for the image")]
		public string Title { get; set; }
		[Display(Name = "Description")]
		[Required(ErrorMessage = "Please enter a description for an image")]
		public string Description { get; set; }
		[Display(Name = "Camera Name")]
		[Required(ErrorMessage = "Please enter the camera name for your photo")]
		public string CameraName { get; set; }
		[Display(Name = "Status")]
		[Required(ErrorMessage = "Please select if you want the photo to be public or private")]
		public string Status { get; set; }
        public DateTime UploadDate { get; set; }
		[Display(Name = "Tag")]
		[Required(ErrorMessage = "Please enter the photo tag")]
		public string Tag { get; set; }

    }
}
