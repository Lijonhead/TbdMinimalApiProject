using System.ComponentModel.DataAnnotations;

namespace TbdMinimalMusicAPi.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set; }



        public List<User> Users { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Song> Songs { get; set; }
    }
}
