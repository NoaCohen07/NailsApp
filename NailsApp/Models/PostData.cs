using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class PostData
    {
        public virtual ICollection<Comment> PostComments { get; set; } = new List<Comment>();
        public int NumLikes { get; set; }
    }
}
