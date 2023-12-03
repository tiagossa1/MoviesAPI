using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class GenderRepository : IGenderRepository
{
    private readonly string _connectionString;
    private readonly IMemoryCache _memoryCache;

    private const string GetAllKey = $"{nameof(GetAll)}_Gender";
    private const string GetByIdKey = $"{nameof(GetById)}_Gender";
    private const string GetByIdsKey = $"{nameof(GetByIds)}_Gender";

    public GenderRepository(IConfiguration configuration, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _connectionString = configuration.GetConnectionString("SqliteConnection");
    }

    public async Task<IList<Gender>> GetAll()
    {
        return await _memoryCache.GetOrCreateAsync(GetAllKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            
            const string sql = "SELECT * FROM Genders";

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
        
            var genders = await connection.QueryAsync<Gender>(sql);
            return genders?.ToList();   
        });
    }

    public async Task<Gender> GetById(long id)
    {
        return await _memoryCache.GetOrCreateAsync(GetByIdKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            
            const string sql = "SELECT * FROM Genders WHERE Id = @id";

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var gender = await connection.QueryFirstOrDefaultAsync<Gender>(sql, new { id });
            return gender;
        });
    }

    public async Task<IEnumerable<Gender>> GetByIds(List<long> ids)
    {
        return await _memoryCache.GetOrCreateAsync(GetByIdsKey, async entry =>
        {
            const string sql = @"SELECT
                                *
                            FROM
                                Genders
                            WHERE
                                Id IN @ids";

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var genders = await connection.QueryAsync<Gender>(sql, new { ids });
            return genders;
        });
    }
}