using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Photo
    {
        public ObjectId Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ObjectId? AlbumId { get; set; }
        public ObjectId UserId { get; set; }
        public string CameraName { get; set; }
        public string Status { get; set; }
        public DateTime UploadDate { get; set; }
        public string Tag { get; set; }
    }
}
