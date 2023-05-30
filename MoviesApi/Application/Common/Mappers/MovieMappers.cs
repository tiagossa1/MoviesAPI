using Application.Common.Dtos;
using Domain.Models;
using Mapster;

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
        return movie?.Adapt<MovieDto>();
    }
}