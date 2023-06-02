using Domain.Models;
using Domain.Services;
using Domain.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AppServices;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commmentRepository;

    public CommentService(ICommentRepository commmentRepository)
    {
        _commmentRepository = commmentRepository;
    }
    public async Task CreateCommentAsync(ObjectId userId, ObjectId photoId, string commentText, DateTime date, Comment comment)
    {
        //var comment = new Comment
        //{
        //    Id = userId,
        //    PhotoId = photoId,
        //    CommentText = commentText,
        //    Date = date,

        //};
        await _commmentRepository.CreateCommentAsync(comment);

    }
    public async Task DeleteCommentAsync(Comment comment)
    {
        await _commmentRepository.DeleteCommentAsync(comment);
    }
}
