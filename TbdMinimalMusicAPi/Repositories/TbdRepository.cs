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

        // This method adds artists to a user.
        public void AddArtists(int userId, List<Artist> artistsToAdd)
        {
            
            try
            {
                var user = _context.Users
                    .Include(u => u.Artists)
                    .FirstOrDefault(u => u.UserId == userId);
                foreach (var artistToAdd in artistsToAdd)
                {
                    user.Artists.Add(artistToAdd);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artists: {ex}");
            }
        }
        // This method adds genres to a specific user. 
        public void AddGenres(List<Genre> genresToAdd, int userId, int artistId)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.Artists)
                    .Include(u => u.Genres)
                    .FirstOrDefault(u => u.UserId == userId);
                var artist = _context.Artists
                    .Include(u => u.Genres)
                    .FirstOrDefault(u => u.ArtistId == artistId);
                foreach (var genreToAdd in genresToAdd)
                {
                    user.Genres.Add(genreToAdd);
                    artist.Genres.Add(genreToAdd);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artists: {ex}");
            }
        }

        // This method adds songs to a user.

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

                    .Include(u => u.Genres)
                    .FirstOrDefault(u => u.UserId == userId);
                var artist = _context.Artists
                    .Include(u => u.Users)
                    .Include(u => u.Songs)
                    .Include(u => u.Genres)
                    .FirstOrDefault(u => u.ArtistId == artistId);
                var genre = _context.Genres
                    .Include(u => u.Users)
                    .Include(u => u.Songs)
                    .Include(u => u.Artists)
                    .FirstOrDefault(u => u.GenreId == genreId);
                foreach (var song in songsToAdd)
r
                {
                    var newsong = new Song
                    {
                        SongTitle = song.SongTitle,

                    }; user.Songs.Add(newsong);

                    };
                    user.Songs.Add(newsong);

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
        //this method gets artists which belong to user
        public List<Artist> GetArtists(int userId)
        {
            User? users = _context.Users
                .Include(x => x.Artists).FirstOrDefault(a => a.UserId==userId); users = new User
                {
                    UserName=users.UserName,
                    Artists=users.Artists.Select(x => new Artist { ArtistId=x.ArtistId, ArtistName=x.ArtistName, ArtistDescription=x.ArtistDescription }).ToList(),
                }; return users.Artists.ToList();
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
