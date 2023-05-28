using Application.People.Command.CreatePeople;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class CreatePeopleCommandValidatorTester
{
    [Test]
    public void ShouldHaveAnErrorIfNamesIsNull()
    {
        var validator = GetValidator();
        var request = new CreatePeopleCommand(null);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Names)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfNamesIsEmpty()
    {
        var validator = GetValidator();
        var request = new CreatePeopleCommand(new List<string>(0));

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Names)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfNamesHaveNullValues()
    {
        var validator = GetValidator();
        var request = new CreatePeopleCommand(new List<string> { null, null });

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Names)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfNamesHaveEmptyValues()
    {
        var validator = GetValidator();
        var request = new CreatePeopleCommand(new List<string> { string.Empty, string.Empty });

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Names)
            .WithErrorCode(ErrorCodesConstants.NOT_EMPTY_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveAnErrorIfNamesHaveMoreThanAllowedMaximumCharacters()
    {
        var validator = GetValidator();
        var request = new CreatePeopleCommand(new List<string> { "GFwxlTVcodHdxwG7w550yyetS9qJkIp0LUVf9HweqY84UwA5dlq9vUP5uyjHv1ASBgUnYDWRc6KYDWGnMYSYWynSkKH7mL3qe3mP7KzXTXaUJofkn8PQaUJy1wHLFrkialOV5kTMo6TEJ81ktHtBmExQEWyiT6sbW1y9aQbroJr5pk7qixlNQLo10FgO84M7Dlg6eCfVWX3qQWHciBXuW1Gt2q81ah0ZQ5GdVpKHQ6j426L3eO7263lir0M6C8AG" });

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Names)
            .WithErrorCode(ErrorCodesConstants.MAXIMUM_LENGTH_VALIDATOR);
    }

    private static CreatePeopleCommandValidator GetValidator() => new CreatePeopleCommandValidator();
}