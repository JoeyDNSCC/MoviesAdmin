using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(256)]
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1024)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(64)]
        [Required]
        public string Genre { get; set; } = string.Empty;
        
        [StringLength(8)]
        [Required]
        public string Rating { get; set; } = string.Empty; // ex. PG-13
        
        [Range(1, 500)]
        [Required]
        public int Runtime { get; set; } // 120min

        [Range(typeof(DateTime), "1880-01-01", "2100-12-31")]
        public DateTime YearReleased { get; set; } // October 31st 
        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp for object creation

    }
}
