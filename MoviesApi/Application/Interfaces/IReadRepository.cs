namespace Application.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task<IList<T>> GetAll();
    Task<T> GetById(long id);
}