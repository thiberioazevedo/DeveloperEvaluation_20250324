using DeveloperEvaluation.Common.Validation;
using FluentValidation.Results;

namespace DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Number { get; set; }
    public BaseEntity()
    {
    }
    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    public virtual ValidationResult GetValidationResult() {
        throw new NotImplementedException();
    }

    public ValidationResultDetail Validate()
    {
        var result = GetValidationResult();

        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            ValidationFailureList = result.Errors
        };
    }
}
