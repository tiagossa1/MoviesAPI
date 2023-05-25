using Domain.Models;

namespace Application.Interfaces;

public interface IGenderRepository : IReadRepository<Gender>
{
    Task<IEnumerable<Gender>> GetByIds(List<long> ids);
}