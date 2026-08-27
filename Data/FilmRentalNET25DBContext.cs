using FilmRentalNET25.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25.Data
{
    public class FilmRentalNET25DBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {

        public FilmRentalNET25DBContext(DbContextOptions<FilmRentalNET25DBContext> options) : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<UsersMovie> UsersMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                    new IdentityRole<int>
                    {
                        Id = 1,
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "7f876c69-a209-4ad3-a5fd-0910c70d7e68"
                    });

        }

    }
}
