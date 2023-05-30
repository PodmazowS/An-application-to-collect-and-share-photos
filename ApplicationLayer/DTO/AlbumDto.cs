using MongoDB.Bson;

namespace ApplicationLayer.DTO;

public class AlbumDto
{
    public ObjectId Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public ObjectId UserId { get; set; }
    public string? Description { get; set; }

    public AlbumDto(ObjectId id, string title, string status, ObjectId userId, string description)
    {
        Id = id;
        Title = title;
        Status = status;
        UserId = userId;
        Description = description;
    }
    
    
}