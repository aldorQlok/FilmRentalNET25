using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<UsersMovie> UsersMovies { get; set; }
    }
}
