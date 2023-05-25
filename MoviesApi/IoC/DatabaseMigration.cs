using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Infrastructure.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DatabaseMigration
{
    public static void Run()
    {
        using var serviceProvider = CreateServices();
        using var scope = serviceProvider.CreateScope();
        
        UpdateDatabase(scope.ServiceProvider);
    }
    
    private static ServiceProvider CreateServices()
    {
        return new ServiceCollection()
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator
                .AddSQLite()
                // Set the connection string
                .WithGlobalConnectionString("Data Source=movies.db")
                // Define the assembly containing the migrations
                .ScanIn(typeof(AddMovieTable).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .Configure<RunnerOptions>(cfg =>
            {
                cfg.Profile = "Seed";
            })
            // Build the service provider
            .BuildServiceProvider(false);
    }

    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    
        // Execute the migrations
        runner.MigrateUp();
    }
}