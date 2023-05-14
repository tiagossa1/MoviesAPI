using Domain.Models;

namespace Application.Interfaces;

public interface IMovieRepository : IReadRepository<Movie>, IWriteRepository<Movie>
{
    
}