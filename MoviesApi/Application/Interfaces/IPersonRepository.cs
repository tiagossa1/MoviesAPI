using Domain.Models;

namespace Application.Interfaces;

public interface IPersonRepository : IReadRepository<Person>, IWriteRepository<Person>
{
    Task<IEnumerable<Person>> Create(List<Person> people);

    Task<IEnumerable<Person>> GetByIds(List<long> ids);

    Task<bool> DoesPersonAlreadyExists(string name);
}