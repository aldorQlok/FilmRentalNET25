using FilmRentalNET25.DTO.Movies;

namespace FilmRentalNET25.Service.IService
{
    public interface IMovieService
    {
        Task<List<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO?> GetMovieByIdAsync(int movieId);
        Task<MovieDTO> CreateMovieAsync(CreateMovieDTO newMovie);
        Task<bool> UpdateMovieAsync(int movieId, UpdateMovieDTO movie);
        Task<bool> DeleteMovieAsync(int movieId);
    }
}
