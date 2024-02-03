using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TbdMinimalMusicAPi.Data;
using TbdMinimalMusicAPi.Models;
using TbdMinimalMusicAPi.Repositories;

namespace TbdRepApiTests
{
    [TestClass]
    public class TbdApiTest
    {
        [TestMethod]
        public void GetUser_Fetch_ExistingUser()
        {
            //Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
              .UseInMemoryDatabase("GetUsers_Fetch_ExistingUser")

              .Options; TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);    //Act    User userToAdd = new User { UserId = 1, UserName = "Dino" };
            context.Users.Add(userToAdd);
            context.SaveChanges(); User result = repo.GetUser(1);    //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userToAdd, result);
            Assert.AreEqual("Dino", result.UserName);
            Assert.AreEqual(context.Users.Count(), 1);
            Assert.AreEqual("Dino", context.Users.Single().UserName);
        }
        [TestMethod]
        public void GetUsers_Fetch_Allusers()
        {
            //Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
              .UseInMemoryDatabase("GetUsers_Fetch_Allusers")
              .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);


            // Act
            User user1 = new User { UserId = 1, UserName = "Dino" };
            User user2 = new User { UserId = 2, UserName = "Alice" };

            context.Users.AddRange(user1, user2);
            context.SaveChanges();

            List<User> userList = repo.GetUsers();

            // Assert
            Assert.IsNotNull(userList);
            Assert.AreEqual(2, userList.Count);

            User? result1 = userList.FirstOrDefault(u => u.UserId == 1);
            User? result2 = userList.FirstOrDefault(u => u.UserId == 2);

            Assert.IsNotNull(result1);
            Assert.AreEqual("Dino", result1.UserName);

            Assert.IsNotNull(result2);
            Assert.AreEqual("Alice", result2.UserName);
        }

        [TestMethod]

        public void GetGenres_Fetch_AllGenres_Related_To_A_User()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("GetGenres_Fetch_AllGenres_Related_To_A_User")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);

            // Act
            Genre genre1 = new Genre { GenreId = 1, Title = "Pop" };
            Genre genre2 = new Genre { GenreId = 2, Title = "Rock" };

            User user = new User
            {
                UserId = 1,
                UserName = "Hugo",
                Genres = new List<Genre> { genre1, genre2 }
            };

            context.Users.Add(user);
            context.SaveChanges();

            List<Genre> genreList = repo.GetGenres(1);

            // Assert
            Assert.IsNotNull(genreList);
            Assert.AreEqual(2, genreList.Count);

            Genre? result1 = genreList.FirstOrDefault(a => a.GenreId == 1);
            Genre? result2 = genreList.FirstOrDefault(a => a.GenreId == 2);

            Assert.IsNotNull(result1);
            Assert.AreEqual("Pop", result1.Title);


            Assert.IsNotNull(result2);
            Assert.AreEqual("Rock", result2.Title);

        }

        [TestMethod]

        public void GetSongs_Fetch_AllSongs_Related_To_A_User()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("GetSongs_Fetch_AllSongs_Related_To_A_User")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);

            // Act
            Song song1 = new Song { SongId = 1, SongTitle = "Off The Wall" };
            Song song2 = new Song { SongId = 2, SongTitle = "In Da Club" };

            User user = new User
            {
                UserId = 1,
                UserName = "Hugo",
                Songs = new List<Song> { song1, song2 }
            };

            context.Users.Add(user);
            context.SaveChanges();

            List<Song> songList = repo.GetSongs(1);

            // Assert
            Assert.IsNotNull(songList);
            Assert.AreEqual(2, songList.Count);

            Song? result1 = songList.FirstOrDefault(a => a.SongId == 1);
            Song? result2 = songList.FirstOrDefault(a => a.SongId == 2);

            Assert.IsNotNull(result1);
            Assert.AreEqual("Off The Wall", result1.SongTitle);


            Assert.IsNotNull(result2);
            Assert.AreEqual("In Da Club", result2.SongTitle);

        }



        [TestMethod]
        public void AddUser_Adds_User_To_Db()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("AddUser_Adds_User_To_Db")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);

            // Act
            User newUser = new User { UserName = "JohnDoe" };
            User addedUser = repo.Adduser(newUser);

            // Assert
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("JohnDoe", addedUser.UserName);

            User retrievedUser = context.Users.FirstOrDefault(u => u.UserId == addedUser.UserId);
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("JohnDoe", retrievedUser.UserName);
        }


        [TestMethod]
        public void AddSongs_Adds_Songs_For_User_Artist_And_Genre()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("AddSongs_Adds_Songs_For_User_Artist_And_Genre")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);

            // Act
            User user = new User { UserId = 1, UserName = "Hugo" };
            Artist artist = new Artist { ArtistId = 1, ArtistName = "Snoop Dogg", ArtistDescription = "HipHop Artist" };
            Genre genre = new Genre { GenreId = 1, Title = "HipHop" };

            context.Users.Add(user);
            context.Artists.Add(artist);
            context.Genres.Add(genre);
            context.SaveChanges();

            List<Song> songsToAdd = new List<Song>
     {
         new Song { SongTitle = "Doggy Dogg World" },
         new Song { SongTitle = "Pump Up" }
     };


            repo.AddSongs(songsToAdd, artistId: 1, userId: 1, genreId: 1);

            // Assert
            User? updatedUser = context.Users
                .Include(u => u.Songs)
                .FirstOrDefault(u => u.UserId == 1);

            Artist? updatedArtist = context.Artists
                .Include(u => u.Songs)
                .FirstOrDefault(u => u.ArtistId == 1);

            Genre? updatedGenre = context.Genres
                .Include(u => u.Songs)
                .FirstOrDefault(u => u.GenreId == 1);


            Assert.IsNotNull(updatedUser, "User not found");
            Assert.IsNotNull(updatedArtist, "Artist not found");
            Assert.IsNotNull(updatedGenre, "Genre not found");


            Assert.AreEqual(2, updatedUser.Songs.Count);
            Assert.AreEqual(2, updatedArtist.Songs.Count);
            Assert.AreEqual(2, updatedGenre.Songs.Count);


            Song? resultSong1 = updatedUser.Songs.FirstOrDefault(s => s.SongTitle == "Doggy Dogg World");
            Song? resultSong2 = updatedUser.Songs.FirstOrDefault(s => s.SongTitle == "Pump Up");

            Assert.IsNotNull(resultSong1);
            Assert.IsNotNull(resultSong2);


            Assert.AreEqual("Doggy Dogg World", resultSong1.SongTitle);
            Assert.AreEqual(artist.ArtistId, resultSong1?.Artist?.ArtistId);
            Assert.AreEqual(genre.GenreId, resultSong1?.Genre?.GenreId);

            Assert.AreEqual("Pump Up", resultSong2.SongTitle);
            Assert.AreEqual(artist.ArtistId, resultSong2?.Artist?.ArtistId);
            Assert.AreEqual(genre.GenreId, resultSong2?.Genre?.GenreId);
        }

        [TestMethod]
        public void AddGenres_Adds_Genres_For_User_Artist()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("AddGenres_Adds_Genres_For_User_Artist")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);

            // Create test data
            User user = new User { UserId = 1, UserName = "Hugo" };
            Artist artist = new Artist { ArtistId = 1, ArtistName = "Snoop Dogg", ArtistDescription = "HipHop Artist" };

            context.Users.Add(user);
            context.Artists.Add(artist);
            context.SaveChanges();

            List<Genre> genresToAdd = new List<Genre>
     {
         new Genre { GenreId = 1, Title = "HipHop" },
         new Genre { GenreId = 2, Title = "Pop" }
     };

            // Act
            repo.AddGenres(genresToAdd, userId: 1, artistId: 1);

            // Assert
            User? updatedUser = context.Users
                .Include(u => u.Genres)
                .FirstOrDefault(u => u.UserId == 1);

            Artist? updatedArtist = context.Artists
                .Include(u => u.Genres)
                .FirstOrDefault(u => u.ArtistId == 1);


            Assert.IsNotNull(updatedUser);
            Assert.IsNotNull(updatedArtist);


            Assert.AreEqual(2, updatedUser.Genres.Count);
            Assert.AreEqual(2, updatedArtist.Genres.Count);


            Genre? resultGenre1 = updatedUser.Genres.FirstOrDefault(g => g.Title == "HipHop");
            Genre? resultGenre2 = updatedUser.Genres.FirstOrDefault(g => g.Title == "Pop");

            Assert.IsNotNull(resultGenre1);
            Assert.IsNotNull(resultGenre2);


            Assert.AreEqual("HipHop", resultGenre1.Title);
            Assert.AreEqual("Pop", resultGenre2.Title);
        }


        [TestMethod]
        public void AddArtists_Adds_Artists_For_User()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("AddArtists_Adds_Artists_For_User")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);


            // Create test data
            User user = new User { UserId = 1, UserName = "Hugo" };
            context.Users.Add(user);
            context.SaveChanges();

            List<Artist> artistsToAdd = new List<Artist>
     {
         new Artist { ArtistId = 1, ArtistName = "Snoop Dogg", ArtistDescription = "HipHop Artist" },
         new Artist { ArtistId = 2, ArtistName = "Dido", ArtistDescription = "Pop Artist" }
     };

            // Act
            repo.AddArtists(userId: 1, artistsToAdd);

            // Assert
            User? updatedUser = context.Users
                .Include(u => u.Artists)
                .FirstOrDefault(u => u.UserId == 1);


            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(2, updatedUser.Artists.Count);


            Artist? resultArtist1 = updatedUser.Artists.FirstOrDefault(a => a.ArtistName == "Snoop Dogg");
            Artist? resultArtist2 = updatedUser.Artists.FirstOrDefault(a => a.ArtistName == "Dido");

            Assert.IsNotNull(resultArtist1);
            Assert.IsNotNull(resultArtist2);


            Assert.AreEqual("Snoop Dogg", resultArtist1.ArtistName);
            Assert.AreEqual("HipHop Artist", resultArtist1.ArtistDescription);

            Assert.AreEqual("Dido", resultArtist2.ArtistName);
            Assert.AreEqual("Pop Artist", resultArtist2.ArtistDescription);
        }


        [TestMethod]

        public void GetArtists_Fetch_AllArtists_Related_To_A_User()
        {
            // Arrange
            DbContextOptions<TbdContext> options = new DbContextOptionsBuilder<TbdContext>()
                .UseInMemoryDatabase("GetArtists_Fetch_AllArtists_Related_To_A_User")
                .Options;

            TbdContext context = new TbdContext(options);
            ITbdRepository repo = new TbdRepository(context);

            // Act
            // Create test data
            Artist artist1 = new Artist { ArtistId = 1, ArtistName = "Dido", ArtistDescription = "Female Pop Artist" };
            Artist artist2 = new Artist { ArtistId = 2, ArtistName = "50 Cent", ArtistDescription = "Male Hip Hop Artist" };

            User user = new User
            {
                UserId = 1,
                UserName = "Hugo",
                Artists = new List<Artist> { artist1, artist2 }
            };

            context.Users.Add(user);
            context.SaveChanges();

            List<Artist> artistList = repo.GetArtists(1);

            // Assert
            Assert.IsNotNull(artistList);
            Assert.AreEqual(2, artistList.Count);

            Artist? result1 = artistList.FirstOrDefault(a => a.ArtistId == 1);
            Artist? result2 = artistList.FirstOrDefault(a => a.ArtistId == 2);

            Assert.IsNotNull(result1);
            Assert.AreEqual("Dido", result1.ArtistName);
            Assert.AreEqual("Female Pop Artist", result1.ArtistDescription);

            Assert.IsNotNull(result2);
            Assert.AreEqual("50 Cent", result2.ArtistName);
            Assert.AreEqual("Male Hip Hop Artist", result2.ArtistDescription);
        }
    }
}
