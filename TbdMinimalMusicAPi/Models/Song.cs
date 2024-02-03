using System.ComponentModel.DataAnnotations;

namespace TbdMinimalMusicAPi.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public string? SongTitle { get; set; }


        public List<User>? Users { get; set; }

        public Artist? Artist { get; set; }


        public Genre? Genre { get; set; }
    }
}
