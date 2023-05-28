using Application.Movies.Command.CreateMovie;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class CreateMovieCastsCommandValidatorTester
{
    [Test]
    public void ShouldHaveErrorsIfCharacterNameIsNull()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(0, 0, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CharacterName)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfCharacterNameIsEmpty()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(0, 0, string.Empty);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CharacterName)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfCharacterNameIsWhiteSpace()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(0, 0, " ");

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CharacterName)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfCharacterNameHasMoreThanMaximumAllowed()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(0, 0, "945m9PrLRJFcE3bBlW675w3nP9uxizq7vDIKnLvf5IcTk5nC989rh4lQz7qi2auuEjbiyILrzbUxlcvvA3aatTDHCGTzNY3lqw4AMfXXWIyVNnJKFL6SLzryyOltxtBNbkPEbuoWkre0bI74nKqO5JbodZw154YLDJQV9q16UwusFxbEXO2zysZvRZ5nYaTMWeM64xQWjX8WvnZ3i0a4N25byII2ZrqgW89pxcQEQ6ox422x4ihz5SbERp3LJ5YO");

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CharacterName)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfGenderIdIsNegative()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(-1, 0, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.GenderId)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfPersonIdIsNegative()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(0, -1, null);

        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.PersonId)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }

    [Test]
    public void ShouldHaveNoErrors()
    {
        var validator = GetValidator();
        var model = new CreateMovieCastsCommand(1, 1, "John Doe");

        var result = validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    private static CreateMovieCastsCommandValidator GetValidator() => new CreateMovieCastsCommandValidator();
}