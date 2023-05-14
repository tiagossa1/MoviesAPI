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
}