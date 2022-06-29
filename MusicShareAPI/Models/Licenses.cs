using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Models
{
    public partial class Licenses
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Reference { get; set; }
        public string Details { get; set; }
        public int Id { get; set; }
    }
}
