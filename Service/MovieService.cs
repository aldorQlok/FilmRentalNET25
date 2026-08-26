using FilmRentalNET25.Data;
using FilmRentalNET25.DTO.Movies;
using FilmRentalNET25.Models;
using FilmRentalNET25.Repository.IRepository;
using FilmRentalNET25.Service.IService;

namespace FilmRentalNET25.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository _movieRepository)
        {
            movieRepository = _movieRepository;
        }
        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await movieRepository.GetAllMoviesAsync();

            var movieDTOs = movies.Select(movie => new MovieDTO
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear
            }).ToList();

            return movieDTOs;
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int movieId)
        {
            var movie = await movieRepository.GetMovieByIdAsync(movieId);

            
            if(movie == null)
            {
                return null;
            }

            var movieDTO = new MovieDTO
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear
            };

            return movieDTO;
        }

        public async Task<MovieDTO> CreateMovieAsync(CreateMovieDTO newMovie)
        {
            var movie = new Movie
            {
                Title = newMovie.Title,
                ReleaseYear = newMovie.ReleaseYear
            };

            var createdMovie = await movieRepository.CreateMovieAsync(movie);

            var movieDTO = new MovieDTO
            {
                MovieId = createdMovie.MovieId,
                Title = createdMovie.Title,
                ReleaseYear = createdMovie.ReleaseYear
            };

            return movieDTO;
        }

        public async Task<bool> UpdateMovieAsync(int movieId, UpdateMovieDTO movie)
        {
            var existingMovie = await movieRepository.GetMovieByIdAsync(movieId);

            if (existingMovie == null)
            {
                return false;
            }

            existingMovie.Title = movie.Title;
            existingMovie.ReleaseYear = movie.ReleaseYear;

            return await movieRepository.UpdateMovieAsync(existingMovie);
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            return await movieRepository.DeleteMovieAsync(movieId);
        }

    }
}
