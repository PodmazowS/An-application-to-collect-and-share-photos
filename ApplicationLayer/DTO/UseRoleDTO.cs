using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class UseRoleDTO
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public string RoleName { get; set; }


        public UseRoleDTO(ObjectId id, ObjectId userId, string roleName)
        {
            Id = id;
            UserId = userId;
            RoleName = roleName;
        }
    }
}
