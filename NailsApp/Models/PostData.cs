using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class PostData
    {
        public List<Comment> PostComments { get; set; }
        public int numLikes { get; set; }
    }
}
