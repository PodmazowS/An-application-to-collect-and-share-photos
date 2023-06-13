using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;

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
            var filter = Builders<Comment>.Filter.Eq("_id", comment.Id);
            await _commentCollection.DeleteOneAsync(filter);
            
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            var comments = await _commentCollection.Find(_ => true).ToListAsync();
            return comments;
        }
        public async Task<Comment> GetById(ObjectId id)
        {
            var filter = Builders<Comment>.Filter.Eq("_id", id);
            return _commentCollection.Find(filter).FirstOrDefault();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForPhoto(ObjectId photoId)
        {
            var filter = Builders<Comment>.Filter.Eq("photoid", photoId);
            return _commentCollection.Find(filter).ToList();
        }
    }
}
