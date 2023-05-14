using Application.Common.Behaviours;
using Application.Interfaces;
using Application.Movies.Command.CreateMovie;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class ConfigureServices
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection service)
    {
        // Validations
        service.AddValidatorsFromAssemblyContaining(typeof(CreateMovieCommandValidator));

        // MediatR
        service.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateMovieCastsCommand>();
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });
        
        // DbContext
        service.AddDbContext<MoviesDbContext>(options => options.UseInMemoryDatabase("MoviesDb"));
        
        // Repositories
        service.AddScoped<IGenderRepository, GenderRepository>();
        service.AddScoped<IGenreRepository, GenreRepository>();
        service.AddScoped<IMovieRepository, MovieRepository>();
        service.AddScoped<IPersonRepository, PersonRepository>();
        
        return service;
    }
}