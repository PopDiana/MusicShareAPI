using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI
{
    public class SongObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
