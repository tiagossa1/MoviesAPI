using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Common.Mappers;

public static class GenreMappers
{
    public static IList<GenreDto> ToDto(this IList<Genre> genres)
    {
        return genres
            ?.Select(ToDto)
            .ToList();
    }

    private static GenreDto ToDto(this Genre genre)
    {
        return genre?.Adapt<GenreDto>();
;    }
}