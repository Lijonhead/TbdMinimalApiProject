using TbdMinimalMusicAPi.Models;

namespace TbdMinimalMusicAPi.Repositories
{
    public interface ITbdRepository
    {
        User Adduser(User user);

        void AddArtists(int userId, List<Artist> artistsToAdd);

        void AddGenres(List<Genre> genres, int userId, int artistId);

        void AddSongs(List<Song> songs, int artistId, int userId, int genreId);

        List<Artist> GetArtists(int userId);

        List<Genre> GetGenres(int userId);

        List<Song> GetSongs(int userId);

        List<User> GetUsers();
        User GetUser(int userId);

        Task<string> GetRelated(int artistId);

        bool UserExists(int userId);

        bool AtristExists(int artistId);

        bool GenreExists(int genreId);
    }
}
