using Application.Common.Dtos;
using Domain.Models;

namespace Application.Common.Mappers;

public static class GenderMappers
{
    public static IEnumerable<GenderDto> ToDto(this IEnumerable<Gender> genres)
    {
        if (genres is null || !genres.Any())
        {
            return null;
        }

        return genres.Select(ToDto);
    }

    public static GenderDto ToDto(this Gender genre)
    {
        if (genre is null)
        {
            return null;
        }

        return new GenderDto(genre.Name);
    }
}