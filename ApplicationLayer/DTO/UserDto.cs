using MongoDB.Bson;

namespace ApplicationLayer.DTO;

public class UserDto
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public UserDto(ObjectId id, string name, string email, string role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;

    }
}