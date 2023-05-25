using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(20230516213200)]
public class AddMovieCastTable : Migration
{
    public override void Up()
    {
        Create.Table("MoviesCast")
            .WithColumn("MovieId").AsInt64().PrimaryKey().ForeignKey("Movies", "Id")
            .WithColumn("GenderId").AsInt64().PrimaryKey().ForeignKey("Genders", "Id")
            .WithColumn("PersonId").AsInt64().PrimaryKey().ForeignKey("People", "Id")
            .WithColumn("CharacterName").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }

    public override void Down()
    {
        Delete.Table("MoviesCast");
    }
}