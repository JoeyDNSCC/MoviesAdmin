using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        // Primary key
        public int Id { get; set; }

        // Foreign keys
        public int GenreId { get; set; }
        public int AudienceRatingId { get; set; }

        [ValidateNever]
        [ScaffoldColumn(false)]
        public List<CriticReview> Reviews { get; set; } = new List<CriticReview>();


        [StringLength(256)]
        [Required]
        public string Title { get; set; } = string.Empty;

        
        [StringLength(1024)]
        [Required]
        public string Description { get; set; } = string.Empty;


        [ValidateNever]
        [ScaffoldColumn(false)]
        public Genre Genre { get; set; } = null!;


        [ValidateNever]
        [ScaffoldColumn(false)]
        public AudienceRating AudienceRating { get; set; } = null!; // ex. PG-13


        [Range(1, 500)]
        [Required]
        [Display(Name = "Run-time")]
        public int Runtime { get; set; } // 120min


        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        //[Range(typeof(DateTime), "01/01/1880", "12/31/2100")] // this doesnt work for some reason so we're skipping validation temporarily
        public DateTime ReleaseDate { get; set; } // October 31st 2001


        // [Required]
        // url to image in azure storage
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Last Modified")] //will change actual varname later
        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp for object creation



    }
}
