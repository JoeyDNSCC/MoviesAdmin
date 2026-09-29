namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty; // ex. PG-13
        public int Runtime { get; set; } // 120min
        public DateTime YearReleased { get; set; } // October 31st 
        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp for object creation

    }
}
