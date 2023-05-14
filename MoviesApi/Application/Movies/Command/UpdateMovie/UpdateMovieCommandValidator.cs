using FluentValidation;

namespace Application.Movies.Command.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
        
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(c => c.Budget)
            .GreaterThan(0);
        
        RuleFor(c => c.HomepageUrl)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(c => c.Plot)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(c => c.ReleaseDate)
            .NotEmpty();
        
        RuleFor(c => c.RuntimeInMinutes)
            .GreaterThan(0);

        RuleForEach(c => c.MovieCasts)
            .SetValidator(new UpdateMovieCastsCommandValidator());

        RuleFor(c => c.RuntimeInMinutes)
            .GreaterThan(0);

        RuleForEach(c => c.Genres)
            .SetValidator(new UpdateMovieGenreCommandValidator());
    }
}