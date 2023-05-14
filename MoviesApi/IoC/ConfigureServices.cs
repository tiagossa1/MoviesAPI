using Application.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class ConfigureServices
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection service)
    {
        service.AddDbContext<MoviesDbContext>(options => options.UseInMemoryDatabase("MoviesDb"));
        
        service.AddScoped<IGenderRepository, GenderRepository>();
        service.AddScoped<IGenreRepository, GenreRepository>();
        service.AddScoped<IMovieRepository, MovieRepository>();
        service.AddScoped<IPersonRepository, PersonRepository>();
        
        return service;
    }
}