using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MoviesDbContext _dbContext;

    public MovieRepository(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _dbContext
            .Movies
            .Include(m => m.MovieCasts)
            .Include(m => m.Genres)
            .ToListAsync();
    }

    public async Task<Movie> GetById(long id)
    {
        return await _dbContext
            .Movies
            .Include(m => m.MovieCasts)
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(movie => movie.Id == id);
    }

    public async Task<Movie> Create(Movie obj)
    {
        var movie = await _dbContext.Movies.AddAsync(obj);
        await _dbContext.SaveChangesAsync();
        
        return movie.Entity;
    }

    public async Task<bool> Update(Movie obj)
    {
        _dbContext.Movies.Update(obj);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Movie obj)
    {
        _dbContext.Movies.Remove(obj);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}