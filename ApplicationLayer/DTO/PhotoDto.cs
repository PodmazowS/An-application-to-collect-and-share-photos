using Domain.Models;
using MongoDB.Bson;

namespace ApplicationLayer.DTO;

public class PhotoDto
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ObjectId? AlbumId { get; set; }
    public string UserId { get; set; }
    public string CameraName { get; set; }
    public string Status { get; set; }
    public DateTime UploadDate { get; set; }
    public string Tag { get; set; }

    public static PhotoDto of(Photo photo)
    {
        if (photo is null)
        {
            return null;
        }
        return new PhotoDto()
        {
            Id = photo.Id.ToString(),
            UserId = photo.UserId.ToString(),
            Title = photo.Title,
            Description = photo.Description,
            Url = photo.Url,
            CameraName = photo.CameraName,
            Status = photo.Status,
            Tag = photo.Tag
        };
    }
}