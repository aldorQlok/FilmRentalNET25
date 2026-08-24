using FilmRentalNET25.Models;

namespace FilmRentalNET25.Repository.IRepository
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task<bool> CreateMovieAsync(Movie newMovie);
        Task<bool> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(Movie movie);
    }
}
