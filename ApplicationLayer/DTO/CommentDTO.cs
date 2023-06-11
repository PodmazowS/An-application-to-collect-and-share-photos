using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace ApplicationLayer.DTO
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PhotoId { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }

        public static CommentDto of(Comment comment)
        {
            if (comment is null)
            {
                return null;
            }

            return new CommentDto()
            {
                Id = comment.Id.ToString(),
                UserId = comment.UserId.ToString(),
                PhotoId = comment.PhotoId.ToString(),
                CommentText = comment.CommentText,
                Date = comment.Date
            };
        }
    }
}
