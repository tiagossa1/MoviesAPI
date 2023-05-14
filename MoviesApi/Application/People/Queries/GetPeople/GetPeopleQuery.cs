using Application.Common.Dtos;
using Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.People.Queries.GetPeople;

public record GetPeopleQuery : IRequest<Result<IEnumerable<PersonDto>>>;

public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, Result<IEnumerable<PersonDto>>>
{
    private readonly ILogger<GetPeopleQueryHandler> _logger;
    private readonly IPersonRepository _personRepository;

    public GetPeopleQueryHandler(IPersonRepository personRepository, ILogger<GetPeopleQueryHandler> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<PersonDto>>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var people = await _personRepository.GetAll();
            if (!people.Any())
            {
                return Result.Ok();
            }
        
            return Result.Ok(people.Select(person => new PersonDto(person.Name)));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There was an error while retrieving people");
            return Result.Fail(e.Message);
        }
    }
}