using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Album
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }//we can use Enum 
        //public Status Status { get; set; }

        public ObjectId UserId { get; set; }
        public string? Description { get; set; }



    }
}
