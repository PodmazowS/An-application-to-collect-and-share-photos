using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class LikeDTO
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId PhotoId { get; set; }
        public int LikeCount { get; set; }

        public LikeDTO(ObjectId id, ObjectId userId, ObjectId photoId, int likeCount) 
        { 
         Id = id;
         UserId = userId;
         PhotoId = photoId;
         LikeCount = likeCount;
        }    


    }
}
