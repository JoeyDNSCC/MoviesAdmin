using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        [Required]
        [Display(Name ="Movie")]
        public int Id { get; set; }

        // foreign key
        [Required]
        public int MovieId { get; set; }

        // navigation property
        [ValidateNever]
        [ScaffoldColumn(false)]
        public Movie Movie { get; set; } = null!;

        [Required]
        [MaxLength(2048)] //give the critics plenty of space to explain their position
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(1,5)] // I havent tested if this is <5 or <=5
        public int Rating { get; set; }

        [Required]
        [Display(Name ="Published")]
        public bool IsPublished { get; set; }


        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
