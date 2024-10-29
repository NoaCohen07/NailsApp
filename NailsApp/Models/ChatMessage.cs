using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class ChatMessage
    {
        public int? SenderId { get; set; }


        public int? ReceiverId { get; set; }


        public int MessageId { get; set; }


        public string? MessageText { get; set; }


        public DateTime MessageTime { get; set; }


        public string? Pic { get; set; }


        public string? Video { get; set; }
    }
}
