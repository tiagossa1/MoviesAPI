using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(20230516212900)]
public class AddGenderTable : Migration
{
    public override void Up()
    {
        Create.Table("Genders")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }

    public override void Down()
    {
        Delete.Table("Genders");
    }
}