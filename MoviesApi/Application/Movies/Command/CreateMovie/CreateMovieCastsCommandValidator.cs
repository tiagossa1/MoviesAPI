using FluentValidation;

namespace Application.Movies.Command.CreateMovie;

public class CreateMovieCastsCommandValidator : AbstractValidator<CreateMovieCastsCommand>
{
    public CreateMovieCastsCommandValidator()
    {
        RuleFor(c => c.CharacterName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(c => c.GenderId)
            .GreaterThan(0);

        RuleFor(c => c.PersonId)
            .GreaterThan(0);
    }
}