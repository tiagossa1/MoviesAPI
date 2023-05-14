namespace Application.Interfaces;

public interface IWriteRepository<T> where T : class
{
    Task<T> Create(T obj);
    Task<bool> Update(T obj);
    Task<bool> Delete(T obj);
}