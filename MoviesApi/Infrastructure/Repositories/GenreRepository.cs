using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MoviesDbContext _dbContext;

    public GenreRepository(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _dbContext.Genres.ToListAsync();
    }

    public async Task<Genre> GetById(long id)
    {
        return await _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
    }
}