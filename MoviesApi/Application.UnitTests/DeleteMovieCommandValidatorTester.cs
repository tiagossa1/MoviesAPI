using Application.Movies.Command.DeleteMovie;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class DeleteMovieCommandValidatorTester
{
    [Test]
    public void ShouldHaveAnErrorIfIdIsNegative()
    {
        var validator = GetValidator();
        var model = new DeleteMovieCommand(-1);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }

    [Test]
    public void ShouldHaveNoErrors()
    {
        var validator = GetValidator();
        var model = new DeleteMovieCommand(1);

        var result = validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    private static DeleteMovieCommandValidator GetValidator() => new DeleteMovieCommandValidator();
}