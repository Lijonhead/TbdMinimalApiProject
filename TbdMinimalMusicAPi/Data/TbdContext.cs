using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Models;

namespace TbdMinimalMusicAPi.Data
{
    public class TbdContext:DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Song> Songs { get; set; }



        public TbdContext(DbContextOptions<TbdContext> options) : base(options)
        {

        }
    }
}
