using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmRentalNET25.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }

        public List<UsersMovie> UsersMovies { get; set; }
    }
}
