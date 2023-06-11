using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.MongoDB;

public class CommentServiceMongoDb : ICommentService
{
    private readonly IMongoCollection<Comment> _comments;
    private readonly MongoClient _client;

    public CommentServiceMongoDb(IOptions<MongoDBSettings> settings)
    {
        _client = new MongoClient(settings.Value.ConnectionUri);
        IMongoDatabase database = _client.GetDatabase(settings.Value.DatabaseName);
        _comments = database.GetCollection<Comment>(settings.Value.CommentCollection);
    }
    public Task CreateCommentAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCommentAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(ObjectId photoId)
    {
        var comments = await _comments
            .Find(Builders<Comment>.Filter.Eq(c => c.PhotoId, photoId))
            .ToListAsync();

        return comments;
    }

    public Task<Comment> GetCommentByIdAsync(ObjectId commentId)
    {
        throw new NotImplementedException();
    }
}