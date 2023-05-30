using Dapper;
using FluentMigrator;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Seeds;

public abstract class BaseSeedMigration : Migration
{
    public override void Up()
    {
    }

    public override void Down()
    {
    }

    protected bool HasTableData(string tableName)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        var query = $"SELECT COUNT(Id) FROM {tableName}";
        return connection.ExecuteScalar<int>(query) > 0;
    }
}