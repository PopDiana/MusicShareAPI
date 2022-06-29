using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Models
{
    public partial class EmailsSent
    {
        public int Type { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Sent { get; set; }
        public int Id { get; set; }
        public string Details { get; set; }
    }
}
