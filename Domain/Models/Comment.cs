using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comment
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        [BsonElement("userid")]
        public ObjectId UserId { get; set; }
        [BsonElement("photoid")]
        public ObjectId PhotoId { get; set; }
        [BsonElement("comment")]
        public string CommentText { get;set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }


    }
}
