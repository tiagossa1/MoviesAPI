using Domain.Models;

namespace Application.Interfaces;

public interface IPersonRepository : IReadRepository<Person>, IWriteRepository<Person>
{
    
}