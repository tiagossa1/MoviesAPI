using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Common.Mappers;

public static class MovieMappers
{
    public static IList<MovieDto> ToDto(this IList<Movie> movies)
    {
        return movies
            ?.Select(ToDto)
            .ToList();
    }

    private static MovieDto ToDto(this Movie movie)
    {
        return movie?.Adapt<MovieDto>();
    }
}