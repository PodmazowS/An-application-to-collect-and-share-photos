using Domain.Models;
using MongoDB.Bson;

namespace ApplicationLayer.DTO;

public class AlbumDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string UserId { get; set; }
    public string? Description { get; set; }

    public static AlbumDto of(Album album)
    {
        if (album is null)
        {
            return null;
        }

        return new AlbumDto()
        {
            Id = album.Id.ToString(),
            Title = album.Title,
            Description = album.Description,
            Status = album.Status,
            UserId = album.UserId.ToString()
        };
    }
    
    
}