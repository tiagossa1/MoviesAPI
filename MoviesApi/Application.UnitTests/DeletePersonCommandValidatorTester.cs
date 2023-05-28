using Application.People.Command.DeletePerson;
using FluentValidation.TestHelper;

namespace Application.UnitTests;

[TestFixture]
public class DeletePersonCommandValidatorTester
{
    [Test]
    public void ShouldHaveErrorsIfIdIsNegative()
    {
        var validator = GetValidator();
        var request = new DeletePersonCommand(-1);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveErrorsIfIdIsZero()
    {
        var validator = GetValidator();
        var request = new DeletePersonCommand(0);

        var result = validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(r => r.Id)
            .WithErrorCode(ErrorCodesConstants.GREATER_THAN_VALIDATOR);
    }
    
    [Test]
    public void ShouldHaveNoErrors()
    {
        var validator = GetValidator();
        var request = new DeletePersonCommand(1);

        var result = validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    private static DeletePersonCommandValidator GetValidator() => new DeletePersonCommandValidator();
}