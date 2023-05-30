using Application.Common.Behaviours;
using Application.Interfaces;
using Application.Movies.Command.CreateMovie;
using FluentValidation;
using Infrastructure.Repositories;
using MediatR;
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
        
        // Caching
        service.AddMemoryCache();
        
        // Repositories
        service.AddScoped<IGenderRepository, GenderRepository>();
        service.AddScoped<IGenreRepository, GenreRepository>();
        service.AddScoped<IMovieRepository, MovieRepository>();
        service.AddScoped<IPersonRepository, PersonRepository>();

        return service;
    }
}