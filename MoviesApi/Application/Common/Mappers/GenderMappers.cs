using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Common.Mappers;

public static class GenderMappers
{
    public static IList<GenderDto> ToDto(this IList<Gender> genres)
    {
        return genres
            ?.Select(ToDto)
            .ToList();
    }

    private static GenderDto ToDto(this Gender genre)
    {
        return genre?.Adapt<GenderDto>();
    }
}