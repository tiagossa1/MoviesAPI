namespace Domain.Models;

public class MovieCast
{
    public long MovieId { get; set; }
    public Movie Movie { get; set; }
    
    public long GenderId { get; set; }
    public Gender Gender { get; set; }
    
    public long PersonId { get; set; }
    public Person Person { get; set; }
    
    public string CharacterName { get; set; }
    
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}