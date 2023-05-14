namespace Domain.Models;

public class MovieCast
{
    public long MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    
    public long GenderId { get; set; }
    public Gender Gender { get; set; } = null!;
    
    public long PersonId { get; set; }
    public Person Person { get; set; } = null!;
    
    public string CharacterName { get; set; } = null!;
}