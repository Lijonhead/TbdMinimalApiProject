using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Data;
using TbdMinimalMusicAPi.Models;

namespace TbdMinimalMusicAPi.Repositories
{
    public class TbdRepository:ITbdRepository
    {
        private readonly TbdContext _context;



        public TbdRepository(TbdContext context)
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
        //this method adds songs to user
        public void AddSongs(List<Song> songsToAdd, int artistId, int userId, int genreId)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.Songs)
                    .Include(u => u.Artists)
                    .Include(u => u.Genres).FirstOrDefault(u => u.UserId == userId); var artist = _context.Artists
                    .Include(u => u.Users)
                    .Include(u => u.Songs)
                    .Include(u => u.Genres)
                    .FirstOrDefault(u => u.ArtistId == artistId); var genre = _context.Genres
                    .Include(u => u.Users)
                    .Include(u => u.Songs)
                    .Include(u => u.Artists).FirstOrDefault(u => u.GenreId == genreId); foreach (var song in songsToAdd)
                {
                    var newsong = new Song
                    {
                        SongTitle = song.SongTitle,
                    }; user.Songs.Add(newsong);
                    artist.Songs.Add(newsong);
                    genre.Songs.Add(newsong);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding songs: {ex}");
            }
        }
        //this method adds new users
        public User Adduser(User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
            }; _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
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
