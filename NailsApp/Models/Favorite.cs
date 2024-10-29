using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class Favorite
    {
        public int? UserId { get; set; }


        public int PostId { get; set; }


        public DateTime SavedTime { get; set; }
    }
}
