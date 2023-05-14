namespace Application.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task<IEnumerable<T?>> GetAll();
    Task<T?> GetById(long id);
}