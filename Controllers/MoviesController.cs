using FilmRentalNET25.DTO.Movies;
using FilmRentalNET25.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmRentalNET25.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;
        public MoviesController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> GetAll()
        {
            var movies = await movieService.GetAllMoviesAsync();

            return Ok(movies);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{movieId:int}")]
        public async Task<ActionResult<MovieDTO>> GetById(int movieId)
        {
            var movie = await movieService.GetMovieByIdAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }


        [HttpPost]
        public async Task<ActionResult<MovieDTO>> Create(CreateMovieDTO newMovie)
        {
            var createdMovie = await movieService.CreateMovieAsync(newMovie);


            return CreatedAtAction(nameof(GetById), new {movieId = createdMovie.MovieId}, createdMovie);
        }

        [HttpPut]
        [Route("{movieId:int}")]
        public async Task<IActionResult> Update(int movieId, UpdateMovieDTO movie)
        {
            var updated = await movieService.UpdateMovieAsync(movieId, movie);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{movieId:int}")]
        public async Task<IActionResult> Delete(int movieId)
        {
            var deleted = await movieService.DeleteMovieAsync(movieId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
