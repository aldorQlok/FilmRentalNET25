namespace FilmRentalNET25.Models
{
    public class UsersMovie
    {
        public int UsersMovieId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }


        public DateTime BrorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
