using Application.Common.Dtos;
using Domain.Models;

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
        if (movieCast is null)
        {
            return null;
        }
        
        return new MovieCastDto(movieCast.Gender.ToDto(), movieCast.Person.ToDto(), movieCast.CharacterName);
    }
}