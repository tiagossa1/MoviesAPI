using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly string _connectionString;

    private const string GetAllKey = $"{nameof(GetAll)}_Genre";
    private const string GetByIdKey = $"{nameof(GetById)}_Genre";

    public GenreRepository(IConfiguration configuration, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _connectionString = configuration.GetConnectionString("SqliteConnection");
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _memoryCache.GetOrCreateAsync(GetAllKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            
            const string sql = "SELECT * FROM Genres";

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var genres = await connection.QueryAsync<Genre>(sql);
            return genres;
        });
    }

    public async Task<Genre> GetById(long id)
    {
        return await _memoryCache.GetOrCreateAsync(GetByIdKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            
            const string sql = "SELECT * FROM Genres WHERE Id = @id";

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var genre = await connection.QueryFirstOrDefaultAsync<Genre>(sql, new { id });
            return genre;
        });
    }
}