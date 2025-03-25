using FluentValidation;

namespace DeveloperEvaluation.Application.CDBs.CreateCDB;

public class CreateCDBCommandValidator : AbstractValidator<CreateCDBCommand>
{
    public CreateCDBCommandValidator()
    {
        RuleFor(user => user.Months).GreaterThan(1).WithMessage("The number of months must be greater than one");
        RuleFor(user => user.Value).GreaterThan(0).WithMessage("The value must be greater than zero");
    }
}