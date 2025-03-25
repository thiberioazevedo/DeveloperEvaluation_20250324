using FluentValidation;

namespace DeveloperEvaluation.WebApi.Features.CDBs.GetCDB;

/// <summary>
/// Validator for GetCDBRequest
/// </summary>
public class GetCDBRequestValidator : AbstractValidator<GetCDBRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCDBRequest
    /// </summary>
    public GetCDBRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CDB ID is required");
    }
}
