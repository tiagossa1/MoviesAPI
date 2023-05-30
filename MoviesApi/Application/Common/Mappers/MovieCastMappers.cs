using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Common.Mappers;

public static class MovieCastMappers
{
    public static IEnumerable<MovieCastDto> ToDto(this IEnumerable<MovieCast> movieCasts)
    {
        if (movieCasts is null || !movieCasts.Any())
        {
            return null;
        }

        return movieCasts.Select(ToDto);
    }

    public static MovieCastDto ToDto(this MovieCast movieCast)
    {
        return movieCast?.Adapt<MovieCastDto>();
    }
}