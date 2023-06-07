using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Photo
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("albumid")]
        public ObjectId? AlbumId { get; set; }
        [BsonElement("userid")]
        public ObjectId UserId { get; set; }
        [BsonElement("cameraname")]
        public string CameraName { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("uploaddate")]
        public DateTime UploadDate { get; set; }
        [BsonElement("tag")]
        public string Tag { get; set; }

        public Photo(ObjectId id, string url, string title, string description, string cameraname, string status, DateTime uploaddate, string tag, ObjectId userid)
        {
            Id = id;
            Url = url;
            Title = title;
            Description = description;
            CameraName = cameraname;
            Status = status;
            UploadDate = uploaddate;
            Tag = tag;
            UserId = userid;
        }
        public Photo()
        {

        }
    }
}
