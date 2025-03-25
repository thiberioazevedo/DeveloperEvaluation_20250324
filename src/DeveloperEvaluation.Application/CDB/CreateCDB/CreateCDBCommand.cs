using DeveloperEvaluation.Common.Validation;
using MediatR;

namespace DeveloperEvaluation.Application.CDBs.CreateCDB;

public class CreateCDBCommand : IRequest<CreateCDBResult>
{
    public decimal Value { get; set; }
    public int Months { get; set; }
    public ValidationResultDetail Validate()
    {
        var validator = new CreateCDBCommandValidator();
        var result = validator.Validate(this);

        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            ValidationFailureList = result.Errors
        };
    }
}