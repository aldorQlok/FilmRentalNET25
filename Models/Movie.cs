using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25.Models
{
    [Index(nameof(Title), nameof(ReleaseYear), IsUnique = true)]
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public List<UsersMovie> UsersMovies { get; set; }
    }
}
