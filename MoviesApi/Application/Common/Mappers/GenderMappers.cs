using Application.Common.Dtos;
using Domain.Models;
using Mapster;

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
        return genre?.Adapt<GenderDto>();
    }
}