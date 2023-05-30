using Application.Common.Behaviours;
using Application.Interfaces;
using Application.Movies.Command.CreateMovie;
using AspNetCoreRateLimit;
using FluentValidation;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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

    public static IServiceCollection AddProductionProjectDependencies(this IServiceCollection service, ConfigurationManager configurationManager)
    {
        // Rate Limit
        service.Configure<IpRateLimitOptions>(options => configurationManager.GetSection("IpRateLimitingSettings").Bind(options));
        
        service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        service.AddInMemoryRateLimiting();

        return service;
    }
    
    public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
    {
        app.UseIpRateLimiting();
        return app;
    }
}