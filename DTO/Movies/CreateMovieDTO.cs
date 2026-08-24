using System.ComponentModel.DataAnnotations;

namespace FilmRentalNET25.DTO.Movies
{
    public class CreateMovieDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "String must be between 4 and 100 characters in length.")]
        public string Title { get; set; }

        [Range(1900, 2999, ErrorMessage ="Release year is invalid")]
        public int ReleaseYear { get; set; }
    }
}
