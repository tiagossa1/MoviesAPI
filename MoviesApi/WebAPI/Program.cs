using Infrastructure;
using IoC;
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

var app = builder.Build();

// This is needed to seed the database.
var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
dbContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.SeedFakeData();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();