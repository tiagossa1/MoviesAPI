namespace Domain.Models;

public class MovieCast
{
    public long MovieId { get; set; }
    
    public long GenderId { get; set; }
    public Gender Gender { get; set; }
    
    public long PersonId { get; set; }
    public Person Person { get; set; }
    
    public string CharacterName { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}