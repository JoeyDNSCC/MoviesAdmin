using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

public class MoviesAdminContext(DbContextOptions<MoviesAdminContext> options) : DbContext(options)
{
    public DbSet<MoviesAdmin.Models.Movie> Movie { get; set; } = default!;
    public DbSet<MoviesAdmin.Models.Genre> Genre { get; set; } = default!;
    public DbSet<MoviesAdmin.Models.AudienceRating> AudienceRating { get; set; } = default!;
    public DbSet<MoviesAdmin.Models.CriticReview> CriticReview { get; set; } = default!;


    // override the contexts default behavior for mapping entities to the DB tables
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // execute parent classes default behavior
        base.OnModelCreating(modelBuilder);

        // change behavior SPECIFICALLY when creating reviews
        modelBuilder.Entity<CriticReview>()
            //create a "composite index" (just compare columns from 2 different tables)
            .HasIndex(r => new { r.MovieId, r.CreatedBy })
            // ensure every row in composite index is unique
            .IsUnique();
    }
}
