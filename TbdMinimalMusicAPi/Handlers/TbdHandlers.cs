using System.Net;
using System.Text.Json;
using TbdMinimalMusicAPi.Models.Dtos;
using TbdMinimalMusicAPi.Models;
using TbdMinimalMusicAPi.Repositories;

namespace TbdMinimalMusicAPi.Handlers
{
    public class TbdHandlers
    {
        // These are respons layer for the methods implemented in the repositories. 
        public static async Task<IResult> GetRelatedResponseAsync(ITbdRepository repo, int artistId)
        {
            try
            {
                var relatedInfo = await repo.GetRelated(artistId);
                if (string.IsNullOrEmpty(relatedInfo))
                {
                    return Results.NotFound($"Related information for Artist with ID {artistId} not found.");
                }
                var relatedDto = JsonSerializer.Deserialize<GetRelatedDto>(relatedInfo);
                return Results.Json(relatedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling related response: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult GetUsernew(ITbdRepository repo, int userId)
        {
            try
            {
                var userDto = repo.GetUser(userId);
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with User ID:{userId} Not Found");
                }
                else
                {
                    var user = new UserDto
                    {
                        UserName = userDto.UserName
                    };
                    return Results.Json(user.UserName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult GetArtistsNew(ITbdRepository repo, int userId)
        {
            try
            {
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with id {userId} not found");
                }
                var artist = repo.GetArtists(userId);
                var artistDtos = artist.Select(artist => new ArtistDto
                {
                    ArtistId = artist.ArtistId,
                    ArtistName = artist.ArtistName,
                    ArtistDescription = artist.ArtistDescription
                }).ToList();
                return Results.Json(artistDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving artists: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult GetUsersNew(ITbdRepository repo)
        {
            try
            {
                var userEntities = repo.GetUsers();
                if (userEntities == null)
                {
                    return Results.NotFound($"No User Data Found In the DB");
                }
                else
                {
                    var userDtos = userEntities.Select(user => new UserDto
                    {
                        UserName = user.UserName
                    }).ToList();
                    return Results.Json(userDtos.Select(user => user.UserName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting users: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult GetGenresNew(ITbdRepository repo, int userId)
        {
            try
            {
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with id {userId} not found");
                }
                var genreEntities = repo.GetGenres(userId);
                var genreDtos = genreEntities.Select(genre => new GenreDto
                {
                    GenreId = genre.GenreId,
                    Title = genre.Title
                }).ToList();
                return Results.Json(genreDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting genres: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult GetSongsNew(ITbdRepository repo, int userId)
        {
            try
            {
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with id {userId} not found");
                }
                var songEntities = repo.GetSongs(userId);
                var songDtos = songEntities.Select(song => new SongDto
                {
                    SongId = song.SongId,
                    SongTitle = song.SongTitle
                }).ToList();
                return Results.Json(songDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting songs: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult AddUser(ITbdRepository repo, User user)
        {
            try
            {
                repo.Adduser(user);
                return Results.Json($"User with User namne {user.UserName.ToUpper()} Added Succesfully ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Not Found:{ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult AddArtists(ITbdRepository repo, int userId, List<Artist> artistsToAdd)
        {
            try
            {
                repo.AddArtists(userId, artistsToAdd);
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with ID {userId} NOT FOUND.");
                }
                else
                {
                    return Results.Json($"Artists added successfully for user with ID {userId}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding artists: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult AddGerens(ITbdRepository repo, List<Genre> genresToAdd, int userId, int artistId)
        {
            try
            {
                repo.AddGenres(genresToAdd, userId, artistId);
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with ID {userId} Not Found ");
                }
                else if (!repo.AtristExists(artistId))
                {
                    return Results.NotFound($"Artist with ID {artistId} Not Found ");
                }
                else
                {
                    return Results.Json($"Genre added successfully for User with ID {userId} \n Artist with ID {artistId}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Genres: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        public static IResult AddSongs(ITbdRepository repo, List<Song> songsToAdd, int artistId, int userId, int genreId)
        {
            try
            {
                repo.AddSongs(songsToAdd, artistId, userId, genreId);
                if (!repo.UserExists(userId))
                {
                    return Results.NotFound($"User with ID {userId} NOT FOUND.");
                }
                else if (!repo.AtristExists(artistId))
                {
                    return Results.NotFound($"Artist with ID {artistId} NOT FOUND.");
                }
                else if (!repo.GenreExists(genreId))
                {
                    return Results.NotFound($"Genre with ID {genreId} NOT FOUND.");
                }
                else
                {
                    return Results.Json($"Songs added successfully for user with ID {userId}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding songs: {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

