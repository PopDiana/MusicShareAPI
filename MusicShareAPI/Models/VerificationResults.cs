using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Models
{
    public partial class VerificationResults
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
        public bool? Approved { get; set; }
        public int Id { get; set; }

    }
}
