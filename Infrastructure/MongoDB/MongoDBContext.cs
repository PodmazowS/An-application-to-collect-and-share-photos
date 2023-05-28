using Domain.Models;
using MongoDB.Bson;
using MongoDB;

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
    public IMongoCollection<Photo> Photos => _database.GetCollection<Photo>("photos");
    // Додайте інші колекції, які використовуєте в проекті

    // Додайте будь-які інші методи або функціональність, яку потребуєте

}