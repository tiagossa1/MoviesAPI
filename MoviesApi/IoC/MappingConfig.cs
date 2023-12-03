using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace IoC;

public class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Movie, MovieDto>
            .ForType()
            .Map(dest => dest.HomepageUrl, src => src.Homepage)
            .Map(dest => dest.ReleaseDate, src => DateOnly.FromDateTime(src.ReleaseDate));
    }
}