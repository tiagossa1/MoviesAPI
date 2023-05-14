using FluentValidation;

namespace Application.Movies.Command.DeleteMovie;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}