using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Data;

namespace TbdMinimalMusicAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionstring = builder.Configuration.GetConnectionString("TbdDb");
            builder.Services.AddDbContext<TbdContext>(option => option.UseSqlServer(connectionstring));
            var app = builder.Build();
      
          


            app.Run();
        }
    }
}
