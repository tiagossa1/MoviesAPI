using Dapper;
using Domain.Models;
using FluentMigrator;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Seeds;

[Profile("Seed")]
public class SeedData : BaseSeedMigration
{
    public override void Up()
    {
        SeedGenders();
        SeedGenres();
    }

    public override void Down()
    {
    }

    private void SeedGenres()
    {
        var hasTableData = HasTableData("Genres");
        if (hasTableData)
        {
            return;
        }

        var insertedGenres = GetGenres();

        var genres = new List<Genre>
        {
            new() { Name = "Action", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Adventure", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Comedy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Drama", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Fantasy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Historical", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Horror", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Musical", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Noir", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Romance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Science Fiction", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Thriller", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { Name = "Western", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };

        var missingGenres = genres.Select(g => g.Name)
            .Where(genre => !insertedGenres.Contains(genre, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        if (missingGenres.Count <= 0) return;
        
        foreach (var genreName in missingGenres)
        {
            Insert.IntoTable("Genres")
                .Row(new { Name = genreName, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        }
    }

    private void SeedGenders()
    {
        var hasTableData = HasTableData("Genders");
        if (hasTableData)
        {
            return;
        }

        var insertedGenders = GetGenders();

        var genders = new List<Gender>
        {
            new() { Name = "Male" },
            new() { Name = "Female" }
        };
        
        var missingGenders = genders.Select(g => g.Name)
            .Where(genre => !insertedGenders.Contains(genre, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        if (missingGenders.Count <= 0) return;
        
        foreach (var genderName in missingGenders)
        {
            Insert.IntoTable("Genders")
                .Row(new { Name = genderName, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        }
    }

    private List<string> GetGenres()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        return connection.Query<string>("SELECT Name FROM Genres")?.ToList() ?? new List<string>(0);
    }

    private List<string> GetGenders()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        return connection.Query<string>("SELECT Name FROM Genders")?.ToList() ?? new List<string>(0);
    }
}