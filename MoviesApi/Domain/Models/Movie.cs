namespace Domain.Models;

public class Movie : BaseEntity
{
    public string Title { get; set; }
    public decimal Budget { get; set; }
    public string Homepage { get; set; }
    public string Plot { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int RuntimeInMinutes { get; set; }

    public List<MovieCast> MovieCasts { get; set; }
    public List<Genre> Genres { get; set; }
}