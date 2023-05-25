using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(20230516213900)]
public class AddMovieTable : Migration
{
    public override void Up()
    {
        Create.Table("Movies")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Budget").AsInt32().NotNullable()
            .WithColumn("Homepage").AsString().Nullable()
            .WithColumn("Plot").AsString().NotNullable()
            .WithColumn("ReleaseDate").AsDateTime2().NotNullable()
            .WithColumn("RuntimeInMinutes").AsInt32().NotNullable()
            .WithColumn("CreatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }

    public override void Down()
    {
        Delete.Table("Movies");
    }
}