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
        var peopleNames = request.Names
            .Select(name => name.Trim())
            .ToList();
        
        var peopleToInsert = await _personRepository.DoesPeopleAlreadyExist(peopleNames);
        var peopleWhoDoesNotExist = peopleToInsert
            .Where(person => !person.Exists)
            .Select(person => new Person
            {
                Name = person.Name
            })
            .ToList();
        
        var people = await _personRepository.Create(peopleWhoDoesNotExist);
        
        return Result.Ok(people.Select(person => person.Id));
    }
}