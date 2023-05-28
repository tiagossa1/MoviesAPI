using Application.Movies.Command.UpdateMovie;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class UpdateMovieCommandValidatorTester
{
    [Test]
    public void ShouldHaveErrorIfIdIsNegative()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, null, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfIdIsZero()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(0, null, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ShouldHaveErrorIfTitleIsNullEmptyOrWhiteSpace(string title)
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, title, 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result
            .ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfTitleHasMoreThanMaximumAllowedLength()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG", 0, null, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfBudgetIsNegative()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, null, -1, null, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result
            .ShouldHaveValidationErrorFor(r => r.Budget)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_OR_EQUAL_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfBudgetIsZero()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, null, -1, null, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result
            .ShouldHaveValidationErrorFor(r => r.Budget)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_OR_EQUAL_VALIDATOR);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ShouldHaveErrorIfPlotIsNullEmptyOrWhiteSpace(string plot)
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, null, 0, "", plot, default, 0, null, null);

        var result = validator.TestValidate(request);
        result
            .ShouldHaveValidationErrorFor(r => r.Plot)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfPlotHasMoreThanMaximumAllowedLength()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", "GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG", default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Plot)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ShouldHaveErrorIfHomepageUrlIsNullEmptyOrWhiteSpace(string homepageUrl)
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, null, 0, homepageUrl, null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result
            .ShouldHaveValidationErrorFor(r => r.HomepageUrl)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfHomepageUrlHasMoreThanMaximumAllowedLength()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG", null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.HomepageUrl)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfReleaseDateHasDefaultValue()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.ReleaseDate)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfRuntimeInMinutesIsNegative()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", null, default, -1, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.RuntimeInMinutes)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfRuntimeInMinutesIsZero()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.RuntimeInMinutes)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfGenresIsNull()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", null, default, 0, null, null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Genres)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfGenresIsEmpty()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", null, default, 0, null, new List<long>(0));

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Genres)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorIfGenresHaveNegativeValues()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(-1, "", 0, 
            "", null, default, 0, null, new List<long> { -1, -2 });

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Genres)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }

    [Test]
    public void ShouldHaveNoErrors()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCommand(1, "Title", 1, 
            "google.com", "Plot", DateTime.Parse("2023-05-28"), 1, new List<UpdateMovieCastsCommand> { new UpdateMovieCastsCommand(1, 1, 1, "John Doe") }, new List<long> { 1, 2 });

        var result = validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static UpdateMovieCommandValidator GetValidator() => new UpdateMovieCommandValidator();
}