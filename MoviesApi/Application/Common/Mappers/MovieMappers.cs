using Application.Common.Dtos;
using Domain.Models;

namespace Application.Common.Mappers;

public static class MovieMappers
{
    public static IEnumerable<MovieDto> ToDto(this IEnumerable<Movie> movies)
    {
        if (movies is null || !movies.Any())
        {
            return null;
        }

        return movies.Select(ToDto);
    }

    public static MovieDto ToDto(this Movie movie)
    {
        if (movie is null)
        {
            return null;
        }

        return new MovieDto(movie.Id, movie.Title, movie.Budget, movie.HomepageUrl, movie.Plot, movie.ReleaseDate, movie.RuntimeInMinutes, movie.MovieCasts.ToDto(), movie.Genres.ToDto());
    }
}