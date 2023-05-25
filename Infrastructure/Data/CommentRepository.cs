using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;

namespace Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        public void Add(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetById(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
