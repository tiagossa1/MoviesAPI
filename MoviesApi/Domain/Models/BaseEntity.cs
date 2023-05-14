using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class BaseEntity
{
    public long Id { get; set; }
    
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}