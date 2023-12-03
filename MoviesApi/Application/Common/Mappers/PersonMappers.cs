using Application.Common.Dtos;
using Domain.Models;
using Mapster;

namespace Application.Common.Mappers;

public static class PersonMappers
{
    public static IList<PersonDto> ToDto(this IList<Person> people)
    {
        return people
            ?.Select(ToDto)
            .ToList();
    }

    private static PersonDto ToDto(this Person person)
    {
        return person?.Adapt<PersonDto>();
    }
}