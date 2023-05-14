using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenderRepository : IGenderRepository
{
    private readonly MoviesDbContext _dbContext;

    public GenderRepository(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Gender?>> GetAll()
    {
        return await _dbContext.Genders.ToListAsync();
    }

    public async Task<Gender?> GetById(long id)
    {
        return await _dbContext.Genders.FirstOrDefaultAsync(gender => gender != null && gender.Id == id);
    }
}