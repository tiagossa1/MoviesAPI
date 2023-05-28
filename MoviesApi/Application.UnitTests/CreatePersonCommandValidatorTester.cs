using Application.People.Command.CreatePerson;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class CreatePersonCommandValidatorTester
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ShouldHaveErrorsIfNameIsNullEmptyOrWhiteSpace(string name)
    {
        var validator = GetValidator();
        var request = new CreatePersonCommand(name);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Name)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfNameHasMoreThanMaximumAllowedCharacters()
    {
        var validator = GetValidator();
        var request = new CreatePersonCommand("GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG");

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Name)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }

    [Test]
    public void ShouldHaveNoErrors()
    {
        var validator = GetValidator();
        var request = new CreatePersonCommand("John Doe");

        var result = validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static CreatePersonCommandValidator GetValidator() => new CreatePersonCommandValidator();
}