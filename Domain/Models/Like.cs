using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Like
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId PhotoId { get; set; }
        public int LikeCount { get; set; }
    }
}
