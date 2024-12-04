using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class Post
    {
        public int? UserId { get; set; }

        public DateTime PostTime { get; set; }

        public string? PostText { get; set; }


        public string Pic { get; set; } = null!;


        public int PostId { get; set; }

        public string PostPicturePath { get; set; } = null!;
    }
}
