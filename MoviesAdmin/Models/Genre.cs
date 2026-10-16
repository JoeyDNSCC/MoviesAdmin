using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Genre
    {
        [Required]
        int Id { get; set; }

        [Required]
        string Title { get; set; } = string.Empty;

    }
}
