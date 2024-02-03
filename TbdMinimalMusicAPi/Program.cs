using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Data;
using TbdMinimalMusicAPi.Handlers;
using TbdMinimalMusicAPi.Repositories;

namespace TbdMinimalMusicAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionstring = builder.Configuration.GetConnectionString("TbdDb");
            builder.Services.AddDbContext<TbdContext>(option => option.UseSqlServer(connectionstring));
            builder.Services.AddScoped<ITbdRepository, TbdRepository>();
            var app = builder.Build();

            //GetEndPoints

            app.MapGet("/GetUser/{userId}", TbdHandlers.GetUsernew);
            app.MapGet("/GetUsers/", TbdHandlers.GetUsersNew);

            app.MapGet("/GetArtists/{userId}", TbdHandlers.GetArtistsNew);

            app.MapGet("/GetRelated/{artistId}", TbdHandlers.GetRelatedResponseAsync);

            app.MapGet("/GetSongs/{userId}", TbdHandlers.GetSongsNew);

            app.MapGet("/GetGenres/{userId}", TbdHandlers.GetGenresNew);





            //postEndpoints

            app.MapPost("/AddUser/", TbdHandlers.AddUser);
            app.MapPost("/AddArtists/{userId}", TbdHandlers.AddArtists);
            app.MapPost("/AddGenres/{userId}/{artistId}", TbdHandlers.AddGerens);
            app.MapPost("/AddSongs/{userId}/{artistId}/{genreId}", TbdHandlers.AddSongs);


            app.Run();
        }
    }
}
