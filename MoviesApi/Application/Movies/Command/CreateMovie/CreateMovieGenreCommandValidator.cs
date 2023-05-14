using FluentValidation;

namespace Application.Movies.Command.CreateMovie;

public class CreateMovieGenreCommandValidator : AbstractValidator<CreateMovieGenreCommand>
{
    public CreateMovieGenreCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}