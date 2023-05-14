using FluentValidation;

namespace Application.Movies.Command.UpdateMovie;

public class UpdateMovieGenreCommandValidator : AbstractValidator<UpdateMovieGenreCommand>
{
    public UpdateMovieGenreCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}