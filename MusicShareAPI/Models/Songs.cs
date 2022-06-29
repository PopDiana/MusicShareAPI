using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShareAPI.Models
{
    public partial class Songs
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? AlbumId { get; set; }
        public string ImageUrl { get; set; }
        public string SongUrl { get; set; }
        public string Title { get; set; }
        public int? Likes { get; set; }
        public int? Plays { get; set; }
        public int? Comments { get; set; }
        public int? GenreId { get; set; }
        public bool? Available { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
