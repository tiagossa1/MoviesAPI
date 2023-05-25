using FluentValidation;

namespace Application.Movies.Command.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(c => c.Budget)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(c => c.HomepageUrl)
            .MaximumLength(255);

        RuleFor(c => c.Plot)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(c => c.ReleaseDate)
            .NotEmpty();

        RuleFor(c => c.RuntimeInMinutes)
            .GreaterThan(0);

        RuleForEach(c => c.GenreIds)
            .GreaterThan(0);

        RuleForEach(c => c.MovieCasts)
            .SetValidator(new CreateMovieCastsCommandValidator());
    }
}