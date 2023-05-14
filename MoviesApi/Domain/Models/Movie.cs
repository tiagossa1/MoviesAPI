namespace Domain.Models;

public class Movie
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Budget { get; set; }
    public string HomepageUrl { get; set; } = null!;
    public string Plot { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public int RuntimeInMinutes { get; set; }

    public IEnumerable<MovieCast> MovieCasts { get; set; } = Enumerable.Empty<MovieCast>();
    public IEnumerable<Genre> Genres { get; set; } = Enumerable.Empty<Genre>();
}