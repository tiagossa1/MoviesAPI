using FluentValidation;

namespace Application.Movies.Command.UpdateMovie;

public class UpdateMovieCastsCommandValidator : AbstractValidator<UpdateMovieCastsCommand>
{
    public UpdateMovieCastsCommandValidator()
    {
        RuleFor(c => c.CharacterName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(c => c.GenderId)
            .GreaterThan(0);

        RuleFor(c => c.PersonId)
            .GreaterThan(0);

        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}