using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class Comment
    {
        public int? PostId { get; set; }


        public DateTime CommentTime { get; set; }


        public string? CommentText { get; set; }


        public int UserId { get; set; }


        public int CommentId { get; set; }
    }
}
