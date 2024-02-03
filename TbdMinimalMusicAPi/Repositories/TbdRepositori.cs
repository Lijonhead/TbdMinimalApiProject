using TbdMinimalMusicAPi.Data;
using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Models;

namespace TbdMinimalMusicAPi.Repositories
{
    public class TbdRepositori:ITbdREpositorics
    {

        private readonly TbdContext _context;
        public TbdRepositori(TbdContext context)
        {
           _context = context; 
        }

        public void AddArtists(int userId, List<Artist> artistsToAdd)
        {
            throw new NotImplementedException();
        }

        public void AddGenres(List<Genre> genres, int userId, int artistId)
        {
            throw new NotImplementedException();
        }

        public void AddSongs(List<Song> songs, int artistId, int userId, int genreId)
        {
            throw new NotImplementedException();
        }

        public User Adduser(User user)
        {
            throw new NotImplementedException();
        }

        public bool AtristExists(int artistId)
        {
            throw new NotImplementedException();
        }

        public bool GenreExists(int genreId)
        {
            throw new NotImplementedException();
        }

        public List<Artist> GetArtists(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetGenres(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRelated(int artistId)
        {
            throw new NotImplementedException();
        }

        public List<Song> GetSongs(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
