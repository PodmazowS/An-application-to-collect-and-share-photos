using Domain.Models;
using MongoDB.Bson;
using MongoDB;
using MongoDB.Driver;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    // Додайте власні властивості, які представляють колекції бази даних

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<UserRole> UserRoles => _database.GetCollection<UserRole>("userroles");
    public IMongoCollection<Photo> Photos => _database.GetCollection<Photo>("photos");
    public IMongoCollection<Album> Albums => _database.GetCollection<Album>("albums");
    public IMongoCollection<Like> Likes => _database.GetCollection<Like>("likes");
    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("comments");
    // Додайте інші колекції, які використовуєте в проекті

    // Додайте будь-які інші методи або функціональність, яку потребуєте

}