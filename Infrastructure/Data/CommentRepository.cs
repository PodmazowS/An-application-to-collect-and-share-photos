using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _commentCollection;

        public CommentRepository(IMongoDatabase database)
        {
            _commentCollection = database.GetCollection<Comment>("comments");
        }
        public void Add(Comment comment)
        {
            _commentCollection.InsertOne(comment);
        }

        public void Delete(ObjectId id)
        {
            _commentCollection.DeleteOne(comment => comment.Id == id);
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            await _commentCollection.InsertOneAsync(comment);
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            await _commentCollection.DeleteOneAsync(c => c.Id == comment.Id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            var comments = await _commentCollection.Find(_ => true).ToListAsync();
            return comments;
        }
        public Comment GetById(ObjectId id)
        {
            return _commentCollection.Find(comment => comment.Id == id).FirstOrDefault();
        }
    }
}
