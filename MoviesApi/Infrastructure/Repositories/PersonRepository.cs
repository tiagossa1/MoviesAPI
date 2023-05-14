using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly MoviesDbContext _dbContext;

    public PersonRepository(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        return await _dbContext.People.ToListAsync();
    }

    public async Task<Person> GetById(long id)
    {
        return await _dbContext.People.FirstOrDefaultAsync(person => person.Id == id);
    }

    public async Task<Person> Create(Person obj)
    {
        var person = await _dbContext.People.AddAsync(obj);
        await _dbContext.SaveChangesAsync();

        return person.Entity;
    }

    public async Task<bool> Update(Person obj)
    {
        _dbContext.People.Update(obj);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(long id)
    {
        var entity = await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
        
        _dbContext.People.Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}