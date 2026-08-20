using FilmRentalNET25.Data;
using FilmRentalNET25.Models;

namespace FilmRentalNET25.Service
{
    public class MovieService
    {
        private readonly FilmRentalNET25DBContext context;

        public MovieService(FilmRentalNET25DBContext _context)
        {
            context = _context;
        }

        public List<Movie> GetMovie()
        {
            var movie = context.Movies.ToList();

            return movie;
        }
    }
}
