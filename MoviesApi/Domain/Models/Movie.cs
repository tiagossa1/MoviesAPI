namespace Domain.Models;

public class Movie : BaseEntity
{
    public string Title { get; set; } = null!;
    public decimal Budget { get; set; }
    public string HomepageUrl { get; set; } = null!;
    public string Plot { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public int RuntimeInMinutes { get; set; }

    public IEnumerable<MovieCast> MovieCasts { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
}