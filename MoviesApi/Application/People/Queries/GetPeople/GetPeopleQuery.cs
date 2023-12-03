using Application.Common.Dtos;
using Application.Common.Mappers;
using Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.People.Queries.GetPeople;

public record GetPeopleQuery : IRequest<Result<IList<PersonDto>>>;

public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, Result<IList<PersonDto>>>
{
    private readonly ILogger<GetPeopleQueryHandler> _logger;
    private readonly IPersonRepository _personRepository;

    public GetPeopleQueryHandler(IPersonRepository personRepository, ILogger<GetPeopleQueryHandler> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<Result<IList<PersonDto>>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var people = await _personRepository.GetAll();
            return Result.Ok(people.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There was an error while retrieving people");
            return Result.Fail(e.Message);
        }
    }
}