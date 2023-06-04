using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDbCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;

namespace Domain.Models
{
    public class UserRole : MongoIdentityRole<ObjectId>
    {
        [BsonElement("userid")]
        public ObjectId UserId { get; set; }

    }
}