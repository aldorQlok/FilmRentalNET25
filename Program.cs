
using FilmRentalNET25.Data;
using FilmRentalNET25.Middleware;
using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<FilmRentalNET25DBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            // Injicera middleware i Request Pipeline
            app.UseMiddleware<SimpleMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
