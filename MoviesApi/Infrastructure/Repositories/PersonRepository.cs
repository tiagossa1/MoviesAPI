using System.Transactions;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Domain.Responses;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly string _connectionString;

    public PersonRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqliteConnection");
    }

    public async Task<IList<Person>> GetAll()
    {
        const string sql = @"SELECT
                                Id,
                                Name,
                                CreatedAt,
                                UpdatedAt
                            FROM People";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var people = await connection.QueryAsync<Person>(sql);
        return people?.ToList();
    }

    public async Task<Person> GetById(long id)
    {
        const string sql = @"SELECT
                                Id, Name, CreatedAt, UpdatedAt
                            FROM
                                People
                            WHERE Id = @id
                            LIMIT 1";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var person = await connection.QueryFirstOrDefaultAsync<Person>(sql, new { id });
        return person;
    }

    public async Task<Person> Create(Person obj)
    {
        const string sql = @"INSERT INTO People(Name) VALUES(@name);
                            SELECT last_insert_rowid();";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var id = await connection.ExecuteScalarAsync<long>(sql, new { name = obj.Name });
        return await GetById(id);
    }

    public async Task<bool> Update(Person obj)
    {
        const string sql = @"UPDATE People
                            SET Name = @name
                            WHERE Id = @id";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await connection.QueryAsync<Person>(sql, new { id = obj.Id, name = obj.Name });
        return true;
    }

    public async Task<bool> Delete(long id)
    {
        const string sql = @"
                            DELETE FROM
                                MoviesCast
                            WHERE
                                PersonId = @id;
                            
                            DELETE FROM
                                People
                            WHERE
                                Id = @id";

        using var transactionScope = new TransactionScope();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(sql, new { id });
        transactionScope.Complete();

        return true;
    }

    public async Task<IEnumerable<Person>> Create(List<Person> people)
    {
        const string sql = @"INSERT INTO People(Name) VALUES(@name);
                            SELECT last_insert_rowid();";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            var newIds = new List<long>();

            foreach (var person in people)
            {
                var id = await connection.ExecuteScalarAsync<long>(sql, new { name = person.Name });
                newIds.Add(id);
            }

            await transaction.CommitAsync();
            return await GetByIds(newIds);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Person>> GetByIds(List<long> ids)
    {
        const string sql = @"SELECT
                                Id,
                                Name,
                                CreatedAt,
                                UpdatedAt
                            FROM
                                People
                            WHERE
                                Id IN @ids";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var people = await connection.QueryAsync<Person>(sql, new { ids });
        return people;
    }

    public async Task<bool> DoesPersonAlreadyExists(string name)
    {
        const string sql = @"SELECT 1 FROM People WHERE Name = @name COLLATE NOCASE";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var exists = await connection.QueryFirstOrDefaultAsync<bool>(sql, new { name });
        return exists;
    }

    public async Task<List<PeopleAlreadyExistResponse>> DoesPeopleAlreadyExist(List<string> names)
    {
        const string sql = @"SELECT Id, Name FROM People WHERE Name IN @names COLLATE NOCASE";

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var people = await connection.QueryAsync<Person>(sql, new { names }) ?? Enumerable.Empty<Person>();
        return names
            .Select(name => new PeopleAlreadyExistResponse(name, people.Any(person => person.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))))
            .ToList();
    }
}