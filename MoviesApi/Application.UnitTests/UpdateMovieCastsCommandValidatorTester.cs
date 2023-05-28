using Application.Movies.Command.UpdateMovie;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class UpdateMovieCastsCommandValidatorTester
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ShouldHaveAnErrorIfCharacterNameIsNullEmptyOrWhiteSpace(string characterName)
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, 0, 0, characterName);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.CharacterName)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfCharacterNameHasMoreThanMaximumAllowedCharacters()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, 0, 0, "945m9PrLRJFcE3bBlW675w3nP9uxizq7vDIKnLvf5IcTk5nC989rh4lQz7qi2auuEjbiyILrzbUxlcvvA3aatTDHCGTzNY3lqw4AMfXXWIyVNnJKFL6SLzryyOltxtBNbkPEbuoWkre0bI74nKqO5JbodZw154YLDJQV9q16UwusFxbEXO2zysZvRZ5nYaTMWeM64xQWjX8WvnZ3i0a4N25byII2ZrqgW89pxcQEQ6ox422x4ihz5SbERp3LJ5YO");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.CharacterName)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfGenreIdIsNegative()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, -1, 0, "");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.GenderId)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfGenreIdIsZero()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, 0, 0, "");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.GenderId)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfPersonIdIsNegative()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, 0, -1, "");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.PersonId)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfPersonIdIsZero()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, 0, 0, "");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.PersonId)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfIdIsNegative()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(-1, 0, 0, "");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfIdIsZero()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(0, 0, 0, "");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }

    [Test]
    public void ShouldNotHaveErrors()
    {
        var validator = GetValidator();
        var request = new UpdateMovieCastsCommand(1, 1, 1, "John Doe");

        var result = validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    private static UpdateMovieCastsCommandValidator GetValidator() => new UpdateMovieCastsCommandValidator();
}