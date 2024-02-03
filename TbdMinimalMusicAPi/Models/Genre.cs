using System.ComponentModel.DataAnnotations;

namespace TbdMinimalMusicAPi.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Title { get; set; }



        public List<User> Users { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Song> Songs { get; set; }
    }
}
