using Application.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.People.Command.CreatePerson;

public record CreatePersonCommand(string Name) : IRequest<Result<long>>;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<long>>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<long>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var personAlreadyExists = await _personRepository.DoesPersonAlreadyExists(request.Name);
        if (personAlreadyExists)
        {
            return Result.Fail(new Error("Could not create the person: already exists in the database."));
        }
        
        var person = await _personRepository.Create(new Person
        {
            Name = request.Name
        });
        
        return Result.Ok(person.Id);
    }
}