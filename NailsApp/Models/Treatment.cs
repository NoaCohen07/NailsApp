using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class Treatment
    {
        public int TreatmentId { get; set; }


        public int? UserId { get; set; }

        public string TreatmentText { get; set; } = null!;

        public int Duration { get; set; }

        public int Price { get; set; }
    }
}
