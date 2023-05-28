using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Like
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        [BsonElement("userid")]
        public ObjectId UserId { get; set; }
        [BsonElement("photoid")]
        public ObjectId PhotoId { get; set; }
        [BsonElement("likecount")]
        public int LikeCount { get; set; }
    }
}
