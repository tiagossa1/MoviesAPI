using Application.Movies.Command.CreateMovie;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class CreateMovieCommandValidatorTester
{
    [Test]
    public void ShouldHaveAnErrorIfTitleIsNull()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfTitleIsEmpty()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(string.Empty, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfTitleIsWhiteSpace()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(" ", 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfTitleHasMoreThanMaximumAllowedCharacters()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand("GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG\n", 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfBudgetIsNegative()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand("", -1, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Budget)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_OR_EQUAL_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfHomepageUrlHasMoreThanMaximumAllowedCharacters()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand("Title", 0, "GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG\n", null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.HomepageUrl)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfPlotIsNull()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Plot)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfPlotIsEmpty()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, string.Empty, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Plot)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfPlotIsWhiteSpace()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, " ", default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Plot)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfPlotHasMoreThanMaximumAllowedCharacters()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, "GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG\n", default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Plot)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfReleaseDateIsDefault()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.ReleaseDate)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfRuntimeInMinutesIsNegative()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, -1, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.RuntimeInMinutes)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfGenreIdsIsNull()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, -1, null, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.GenreIds)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfGenreIdsIsEmpty()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, -1, new List<long>(0), null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.GenreIds)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfGenreIdsHasNegativeValues()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, -1, new List<long> { -1, -2 }, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.GenreIds)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfMovieCastsIsNull()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, -1, new List<long> { -1, -2 }, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.MovieCasts)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfMovieCastsIsEmpty()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand(null, 0, null, null, default, -1, new List<long> { -1, -2 }, new List<CreateMovieCastsCommand>(0));

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.MovieCasts)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }

    [Test]
    public void ShouldHaveNoErrors()
    {
        var validator = GetValidator();
        var model = new CreateMovieCommand("Title", 500, "google.com", "Plot", DateTime.Parse("2023-05-27"), 150, new List<long> { 1, 2 }, new List<CreateMovieCastsCommand> { new(1, 1, "John Doe") });

        var result = validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static CreateMovieCommandValidator GetValidator() => new CreateMovieCommandValidator();
}