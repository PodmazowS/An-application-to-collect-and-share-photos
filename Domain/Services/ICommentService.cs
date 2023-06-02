using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICommentService
    {

        public Task CreateCommentAsync(ObjectId userId, ObjectId photoId, string commentText, DateTime date, Comment comment);
        public Task DeleteCommentAsync(Comment comment);

    }
}
