using FluentValidation;

namespace Application.People.Command.DeletePerson;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}