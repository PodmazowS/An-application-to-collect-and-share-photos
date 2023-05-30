using MongoDB.Bson;

namespace ApplicationLayer.DTO;

public class PhotoDto
{
    public ObjectId Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ObjectId? AlbumId { get; set; }
    public ObjectId UserId { get; set; }
    public string CameraName { get; set; }
    public string Status { get; set; }
    public DateTime UploadDate { get; set; }
    public string Tag { get; set; }

    public PhotoDto(ObjectId id, string url, string title, string description, ObjectId albumId,
        ObjectId userId, string cameraName, string status, DateTime uploadDate, string tag)
    {
        Id = id;
        Url = url;
        Title = title;
        Description = description;
        AlbumId = albumId;
        UserId = userId;
        CameraName = cameraName;
        Status = status;
        UploadDate = uploadDate;
        Tag = tag;
    }
}