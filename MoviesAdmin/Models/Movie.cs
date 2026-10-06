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

        [StringLength(16)]
        [Required]
        public string Genre { get; set; } = string.Empty;
        
        [StringLength(8)]
        [Required]
        public string Rating { get; set; } = string.Empty; // ex. PG-13
        
        [Range(1, 500)]
        [Required]
        [Display(Name = "Run-time")]
        public int Runtime { get; set; } // 120min

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        //[Range(typeof(DateTime), "01/01/1880", "12/31/2100")] // this doesnt work for some reason so we're skipping validation temporarily
        public DateTime ReleaseDate { get; set; } // October 31st 2001

        [Display(Name = "Last Modified")] //will change actual varname later
        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp for object creation

    }
}
