using FilmRentalNET25.Data;
using FilmRentalNET25.Models;
using FilmRentalNET25.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly FilmRentalNET25DBContext context;

        public MovieRepository(FilmRentalNET25DBContext _context)
        {
            context = _context;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            var movies = await context.Movies.AsNoTracking().ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.MovieId == movieId);

            return movie;
        }

        public async Task<Movie> CreateMovieAsync(Movie newMovie)
        {
            context.Movies.Add(newMovie);
            await context.SaveChangesAsync();

            return newMovie;
        }

        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            context.Movies.Update(movie);
            var result = await context.SaveChangesAsync();

            if(result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            var rowsAffected = await context.Movies.Where(m => m.MovieId == movieId).ExecuteDeleteAsync();

            if(rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

    }
}
