using System.ComponentModel.DataAnnotations;

namespace TbdMinimalMusicAPi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }



        public List<Artist> Artists { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Song> Songs { get; set; }
    }
}
