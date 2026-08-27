using Scalar.AspNetCore;
using FilmRentalNET25.Data;
using FilmRentalNET25.Middleware;
using Microsoft.EntityFrameworkCore;
using FilmRentalNET25.Repository.IRepository;
using FilmRentalNET25.Repository;
using FilmRentalNET25.Service.IService;
using FilmRentalNET25.Service;
using FilmRentalNET25.Models;
using Microsoft.AspNetCore.Identity;

namespace FilmRentalNET25
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<FilmRentalNET25DBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentityApiEndpoints<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<FilmRentalNET25DBContext>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            //await app.SeedAdminUser();

            //app.UseMiddleware<GlobalExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            // Injicera middleware i Request Pipeline
            app.UseMiddleware<SimpleMiddleware>();

            app.MapIdentityApi<User>();

            app.MapControllers();

            app.Run();
        }
    }
}
