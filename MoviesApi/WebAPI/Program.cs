using HealthChecks.UI.Client;
using IoC;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movies API - Test only",
        Description = "Movies API made in .NET 6"
    });
});

builder.Services.AddProjectDependencies();

builder.Services.AddHealthChecks().AddSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    DatabaseMigration.Run();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();