namespace Domain.Models;

public class Genre : BaseEntity
{
    public string Name { get; set; }

    public IEnumerable<Movie> Movies { get; set; }
}