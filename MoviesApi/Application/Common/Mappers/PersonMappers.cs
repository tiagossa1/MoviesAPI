using Application.Common.Dtos;
using Domain.Models;

namespace Application.Common.Mappers;

public static class PersonMappers
{
    public static IEnumerable<PersonDto> ToDto(this IEnumerable<Person> people)
    {
        if (people is null || !people.Any())
        {
            return null;
        }

        return people.Select(ToDto);
    }

    public static PersonDto ToDto(this Person person)
    {
        if (person is null)
        {
            return null;
        }

        return new PersonDto(person.Name);
    }
}