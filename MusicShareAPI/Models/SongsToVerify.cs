using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Models
{
    public partial class SongsToVerify
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public int Id { get; set; }
    }
}
