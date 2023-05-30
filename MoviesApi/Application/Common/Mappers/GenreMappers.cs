using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Common.Mappers;

public static class GenreMappers
{
    public static IEnumerable<GenreDto> ToDto(this IEnumerable<Genre> genres)
    {
        if (genres is null || !genres.Any())
        {
            return null;
        }

        return genres.Select(ToDto);
    }

    public static GenreDto ToDto(this Genre genre)
    {
        return genre?.Adapt<GenreDto>();
;    }
}