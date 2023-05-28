using FluentValidation;

namespace Application.People.Command.CreatePeople;

public class CreatePeopleCommandValidator : AbstractValidator<CreatePeopleCommand>
{
    public CreatePeopleCommandValidator()
    {
        RuleFor(c => c.Names)
            .NotEmpty();
        
        RuleForEach(c => c.Names)
            .NotEmpty()
            .MaximumLength(255);
    }
}