using FilmRentalNET25.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25.Data
{
    public class FilmRentalNET25DBContext : DbContext
    {

        public FilmRentalNET25DBContext(DbContextOptions<FilmRentalNET25DBContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UsersMovie> UsersMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasData(
                    new User { UserId = 1, Name = "Aldor", Email = "Aldor@gmail.com" },
                    new User { UserId = 2, Name = "Johan", Email = "Johan@gmail.com" }
                );

            // property index med unik index.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Composite Index med unik index.
            modelBuilder.Entity<Movie>()
                .HasIndex(m => new { m.Title, m.ReleaseYear })
                .IsUnique();

        }

    }
}
