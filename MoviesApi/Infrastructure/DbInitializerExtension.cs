using Bogus;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Person = Domain.Models.Person;

namespace Infrastructure;

public static class DbInitializerExtension
{
    public static IApplicationBuilder SeedFakeData(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<MoviesDbContext>();

            var people = GeneratePeople();
            var movies = GenerateMovies(context);
            
            context.People.AddRange(people);
            context.Movies.AddRange(movies);
            context.SaveChanges();

        }
        catch (Exception)
        {
        }

        return app;
    }

    private static IEnumerable<Person> GeneratePeople()
    {
        var people = new List<Person>();
        
        for (var i = 0; i < 100; i++)
        {
            people.Add(new Person
            {
                Id = i+1,
                Name = new Faker().Person.FullName
            });
        }

        return people;
    }

    private static IEnumerable<Movie> GenerateMovies(MoviesDbContext ctx)
    {
        var movies = new List<Movie>();
        
        for (var i = 0; i < 100; i++)
        {
            var randomGenreId = new Faker().Random.Number(1, 5);
            var genre = ctx.Genres.FirstOrDefault(g => g.Id == randomGenreId);
            
            movies.Add(new Movie
            {
                Id = i+1,
                Budget = new Faker().Random.Number(10000, 100000),
                Genres = new List<Genre>
                {
                    genre
                },
                Plot = new Faker().Lorem.Paragraph(1),
                Title = new Faker().Lorem.Sentence(1),
                HomepageUrl = new Faker().Lorem.Sentence(1),
                ReleaseDate = new Faker().Date.Future(),
                RuntimeInMinutes = new Faker().Random.Number(100, 200),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow,
                MovieCasts = new List<MovieCast>
                {
                    new MovieCast
                    {
                        PersonId = new Faker().Random.Number(1, 100), GenderId = new Faker().Random.Number(1, 2), CreatedAtUtc = DateTime.UtcNow,
                         UpdatedAtUtc = DateTime.UtcNow, CharacterName = new Faker().Person.FullName
                    }
                }
            });
        }

        return movies;
    }
}