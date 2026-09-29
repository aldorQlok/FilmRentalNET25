using FilmRentalNET25.Data;
using FilmRentalNET25.DTO.Movies;
using FilmRentalNET25.Models;
using FilmRentalNET25.Repository.IRepository;
using FilmRentalNET25.Service.IService;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace FilmRentalNET25.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        //private readonly IMemoryCache cache;

        private readonly IDistributedCache cache;
        private const string moviesCacheKey = "movies";

        public MovieService(IMovieRepository _movieRepository, IDistributedCache _cache)
        {
            movieRepository = _movieRepository;

            cache = _cache;
        }
        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            //if(cache.TryGetValue(moviesCacheKey, out List<MovieDTO> cachedMovies))
            //{
            //    Console.WriteLine("Cache Hit");

            //    return cachedMovies;
            //}

            var cachedMovies = await cache.GetStringAsync(moviesCacheKey);
            if(cachedMovies != null)
            {
                Console.WriteLine("Cache Hit");

                var cachedMoviesList = JsonSerializer.Deserialize<List<MovieDTO>>(cachedMovies);

                return cachedMoviesList;
            }


            Console.WriteLine("Cache Miss");

            var movies = await movieRepository.GetAllMoviesAsync();

            var movieDTOs = movies.Select(movie => new MovieDTO
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear
            }).ToList();

            var json = JsonSerializer.Serialize(movieDTOs);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            };

            await cache.SetStringAsync(moviesCacheKey, json, options);

            //cache.Set(moviesCacheKey, movieDTOs, TimeSpan.FromSeconds(30));

            return movieDTOs;
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int movieId)
        {

            //if(cache.TryGetValue(cachedMovieKey, out MovieDTO cachedMovie))
            //{
            //    Console.WriteLine("Cache Hit");
            //    return cachedMovie;
            //}

            Console.WriteLine("Cache Miss");

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

            //cache.Set(cachedMovieKey, movieDTO, TimeSpan.FromSeconds(30));

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
            //string cachedMovieKey = $"movie:{movieId}";

            var existingMovie = await movieRepository.GetMovieByIdAsync(movieId);

            if (existingMovie == null)
            {
                return false;
            }

            existingMovie.Title = movie.Title;
            existingMovie.ReleaseYear = movie.ReleaseYear;

            var updated = await movieRepository.UpdateMovieAsync(existingMovie);

            //if (updated)
            //{
            //    cache.Remove(cachedMovieKey);
            //    cache.Remove(moviesCacheKey);
            //}

            return updated;
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            //string cachedMovieKey = $"movie:{movieId}";

            var deleted = await movieRepository.DeleteMovieAsync(movieId);

            //if(deleted)
            //{
            //    cache.Remove(moviesCacheKey);
            //    cache.Remove(cachedMovieKey);
            //    Console.WriteLine("Cache cleared");
            //}

            if (deleted)
            {
                await cache.RemoveAsync(moviesCacheKey);
            }

            return deleted;
        }

    }
}
