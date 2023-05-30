using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public  class CommentDTO
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId PhotoId { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }

        public CommentDTO(ObjectId id, ObjectId userId, ObjectId photoId, string commentText, DateTime date)
        {
            Id = id;
            UserId = userId;
            PhotoId = photoId;
            CommentText = commentText;
            Date = date;

        }
    }
}
