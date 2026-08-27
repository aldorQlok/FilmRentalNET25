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

        }

    }
}
