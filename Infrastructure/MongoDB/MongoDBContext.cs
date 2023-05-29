using Domain.Models;
using MongoDB.Bson;
using MongoDB;
using MongoDB.Driver;
using Infrastructure.MongoDB;
using Microsoft.Extensions.Options;

public class MongoDBContext
{
    private readonly IMongoCollection<User> _users;
    private readonly IMongoCollection<UserRole> _userroles;
    private readonly IMongoCollection<Photo> _photos;
    private readonly IMongoCollection<Album> _albums;
    private readonly IMongoCollection<Like> _likes;
    private readonly IMongoCollection<Comment> _comments;
    private readonly MongoClient _client;
    public MongoDBContext(IOptions<MongoDBSettings> settings)
    {
        _client = new MongoClient(settings.Value.ConnectionUri);
        IMongoDatabase database = _client.GetDatabase(settings.Value.DatabaseName);
        _users = database.GetCollection<User>(settings.Value.UserCollection);
        _userroles = database.GetCollection<UserRole>(settings.Value.UserRoleCollection);
        _photos = database.GetCollection<Photo>(settings.Value.PhotoCollection);
        _albums = database.GetCollection<Album>(settings.Value.AlbumCollection);
        _likes = database.GetCollection<Like>(settings.Value.LikeCollection);
        _comments = database.GetCollection<Comment>(settings.Value.CommentCollection);
    }
    /* public IMongoCollection<Photo> Photos => _photos; */


    // Додайте будь-які інші методи або функціональність, яку потребуєте

}