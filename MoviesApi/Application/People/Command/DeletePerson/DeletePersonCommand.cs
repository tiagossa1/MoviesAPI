using Application.Interfaces;
using FluentResults;
using MediatR;

namespace Application.People.Command.DeletePerson;

public record DeletePersonCommand(long Id) : IRequest<Result>;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var result = await _personRepository.Delete(request.Id);

        return result ? Result.Ok() : Result.Fail(new Error($"Could not delete person with id {request.Id}."));
    }
}