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
    public async Task CreateCommentAsync(Comment comment)
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

    public async Task<Comment> GetCommentByIdAsync(ObjectId commentId)
    {
        return await _commmentRepository.GetById(commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(ObjectId photoId)
    {
        var Comments = await _commmentRepository.GetCommentsForPhoto(photoId);
        return Comments;
    }
}
