using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Data;
using TbdMinimalMusicAPi.Models;

namespace TbdMinimalMusicAPi.Repositories
{
    public class TbdRepository : ITbdRepository
    {
        private readonly TbdContext _context;



        public TbdRepository(TbdContext context)
        {
            _context = context;
        }


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



        void ITbdRepository.AddGenres(List<Genre> genresToAdd, int userId, int artistId)
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

        public void AddSongs(List<Song> songsToAdd, int artistId, int userId, int genreId)
        {

            try
            {
                var user = _context.Users
                    .Include(u => u.Songs)
                    .Include(u => u.Artists)
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
                {
                    var newsong = new Song
                    {
                        SongTitle = song.SongTitle,


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

        public User Adduser(User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
            };


            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public List<Artist> GetArtists(int userId)
        {
            User? users = _context.Users
                 .Include(x => x.Artists)

                 .FirstOrDefault(a => a.UserId == userId);



            users = new User
            {
                UserName = users.UserName,
                Artists = users.Artists.Select(x => new Artist { ArtistId = x.ArtistId, ArtistName = x.ArtistName, ArtistDescription = x.ArtistDescription }).ToList(),
            };


            return users.Artists.ToList();
        }

        public List<Genre> GetGenres(int userId)
        {
            User? users = _context.Users
       .Include(x => x.Genres)

       .FirstOrDefault(a => a.UserId == userId);


            users = new User
            {
                UserName = users.UserName,
                Genres = users.Genres.Select(x => new Genre { GenreId = x.GenreId, Title = x.Title }).ToList(),
            };


            return users.Genres.ToList();
        }

        public List<Song> GetSongs(int userId)
        {
            User? users = _context.Users
         .Include(x => x.Songs)

         .FirstOrDefault(a => a.UserId == userId);



            users = new User
            {
                UserName = users.UserName,
                Songs = users.Songs.Select(x => new Song { SongId = x.SongId, SongTitle = x.SongTitle }).ToList(),
            };


            return users.Songs.ToList();
        }

        public User GetUser(int userId)
        {
            var user = _context.Users

               .FirstOrDefault(p => p.UserId == userId);

            return user;
        }

        public List<User> GetUsers()
        {

            return _context.Users.ToList();

        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }

        public bool AtristExists(int artistId)
        {
            return _context.Artists.Any(u => u.ArtistId == artistId);
        }

        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(u => u.GenreId == genreId);
        }

        public async Task<string> GetRelated(int artistId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://deezerdevs-deezer.p.rapidapi.com/artist/{artistId}";

                    client.BaseAddress = new Uri(apiUrl);

                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "d7e9322450msh50722105539f988p17a776jsn08e2a7b82309");
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "deezerdevs-deezer.p.rapidapi.com");

                    var response = await client.GetAsync(apiUrl);

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {

                    Console.WriteLine($"Error making HTTP request: {ex.Message}");
                    throw;
                }

            }
        }
    }
}

