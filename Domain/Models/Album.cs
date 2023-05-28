using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Album
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }//we can use Enum 
        //public Status Status { get; set; }
        [BsonElement("userid")]
        public ObjectId UserId { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }



    }
}
