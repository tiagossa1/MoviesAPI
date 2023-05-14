using Application.People.Queries.GetPeople;

namespace Application.Common.Dtos;

public record MovieCastDto(GenderDto Gender, PersonDto Person, string CharacterName);