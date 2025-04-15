using DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace DeveloperEvaluation.Domain.Validation;

public class CDBValidator : AbstractValidator<CDB>
{
    public CDBValidator()
    {
        RuleFor(user => user.Months).GreaterThan(1).WithMessage("The number of months must be greater than one");
        RuleFor(user => user.Value).GreaterThan(0).WithMessage("The value must be greater than zero");
        RuleFor(user => user.MonthCDBCollection).NotNull().NotEmpty();

        RuleFor(sale => sale.MonthCDBCollection).Must(x => x?.Count > 0).WithMessage("There must be movement");
        RuleForEach(sale => sale.MonthCDBCollection).ChildRules(child => child.RuleFor(x => x.Month).GreaterThan(0).WithMessage("Month must be greater than zero"));
        RuleForEach(sale => sale.MonthCDBCollection).ChildRules(child => child.RuleFor(x => x.InitialValue).GreaterThan(0).WithMessage("Initial value must be greater than zero"));
    }
}
