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

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.ConfigureApplicationCookie(option =>
                {
                    option.Cookie.SameSite = SameSiteMode.None;
                    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
            }

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy.WithOrigins(builder.Configuration["Frontend_Domain"]) // lägg domänen i User Secrets
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.UseCors("Frontend");

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

            var api = app.MapGroup("/api");

            api.MapIdentityApi<User>();

            app.MapControllers();

            app.Run();
        }
    }
}
