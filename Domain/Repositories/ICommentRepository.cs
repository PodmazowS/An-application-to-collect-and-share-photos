﻿using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICommentRepository
    {
        //Create
        void Add(Comment comment);

        //GetAll
        Task<IEnumerable<Comment>> GetAll();

        //GetById
        Comment GetById(ObjectId id);

        //Update
        //Comment Update(ObjectId id, Comment newComment);

        //Delete
        void Delete(ObjectId id);
    }
}