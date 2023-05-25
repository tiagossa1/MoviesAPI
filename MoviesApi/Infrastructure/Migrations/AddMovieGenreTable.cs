using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(20230516213700)]
public class AddMovieGenreTable : Migration
{
    public override void Up()
    {
        Create.Table("MoviesGenres")
            .WithColumn("MovieId").AsInt64().PrimaryKey().ForeignKey("Movies", "Id")
            .WithColumn("GenreId").AsInt64().PrimaryKey().ForeignKey("Genres", "Id")
            .WithColumn("CreatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }

    public override void Down()
    {
        Delete.Table("MoviesGenres");
    }
}