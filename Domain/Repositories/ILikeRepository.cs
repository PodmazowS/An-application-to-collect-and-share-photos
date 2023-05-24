using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILikeRepository
    {
        //Create
        public int AddLikeToPost(Like like, ObjectId id);

        //GetAll
        //GetById
        //Update?
        //Delete
        public int DeleteLikeFromPost(Like like, ObjectId id);
    }
}
