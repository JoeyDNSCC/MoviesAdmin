using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class AudienceRating
    {
        [Required]
        int Id { get; set; }

        [Required]
        string Title { get; set; } = string.Empty;

    }
}
