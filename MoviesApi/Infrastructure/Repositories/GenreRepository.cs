using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly string _connectionString;

    public GenreRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqliteConnection");
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        const string sql = "SELECT * FROM Genres";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var genres = await connection.QueryAsync<Genre>(sql);
        return genres;
    }

    public async Task<Genre> GetById(long id)
    {
        const string sql = "SELECT * FROM Genres WHERE Id = @id";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        var genre = await connection.QueryFirstOrDefaultAsync<Genre>(sql, new { id });
        return genre;
    }
}