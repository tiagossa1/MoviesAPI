using Application.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.People.Command.CreatePeople;

public record CreatePeopleCommand(List<string> Names) : IRequest<Result<IEnumerable<long>>>;

public class CreatePeopleCommandHandler : IRequestHandler<CreatePeopleCommand, Result<IEnumerable<long>>>
{
    private readonly IPersonRepository _personRepository;

    public CreatePeopleCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<IEnumerable<long>>> Handle(CreatePeopleCommand request, CancellationToken cancellationToken)
    {
        var people = await _personRepository.Create(request.Names
            .Select(name => new Person { Name = name })
            .ToList());
        
        return Result.Ok(people.Select(person => person.Id));
    }
}