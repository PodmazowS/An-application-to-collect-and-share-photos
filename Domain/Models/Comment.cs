using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comment
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId PhotoId { get; set; }
        public string _Comment { get;set; }
        public DateTime Date { get; set; }




    }
}
