using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class GenderRepository : IGenderRepository
{
    private readonly string _connectionString;

    public GenderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqliteConnection");
    }

    public async Task<IEnumerable<Gender>> GetAll()
    {
        const string sql = "SELECT * FROM Genders";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var genders = await connection.QueryAsync<Gender>(sql);
        return genders;
    }

    public async Task<Gender> GetById(long id)
    {
        const string sql = "SELECT * FROM Genders WHERE Id = @id";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var gender = await connection.QueryFirstOrDefaultAsync<Gender>(sql, new { id });
        return gender;
    }

    public async Task<IEnumerable<Gender>> GetByIds(List<long> ids)
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
    }
}