using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [StringLength(64)]
        [Required]
        public string Description { get; set; } = string.Empty;
        [StringLength(255)]
        [Required]
        public string Genre { get; set; } = string.Empty;
        [StringLength(32)]
        [Required]
        public string Rating { get; set; } = string.Empty; // ex. PG-13
        [StringLength(8)]
        [Required]
        public int Runtime { get; set; } // 120min

        public DateTime YearReleased { get; set; } // October 31st 
        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp for object creation

    }
}
