using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TbdMinimalMusicAPi.Data;
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
            builder.Services.AddScoped<ItbdRepositori, TbdRepositori>();
            var app = builder.Build();


            app.Run();
        }
    }
}
