using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        [Required]
        int Id { get; set; }

        [Required]
        [MaxLength(2048)] //give the critics plenty of space to explain their position
        string Description { get; set; }

        [Required]
        [Range(1,5)] // I havent tested if this is <5 or <=5
        int Rating { get; set; }

        [Required]
        bool IsPublished { get; set; }

        [Required]
        string CreatedBy { get; set; } //not sure how our users are going to be stored / formatted yet

        [Required]
        DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
