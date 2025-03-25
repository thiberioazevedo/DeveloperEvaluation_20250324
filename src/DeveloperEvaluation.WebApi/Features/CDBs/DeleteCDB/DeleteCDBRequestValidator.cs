using FluentValidation;

namespace DeveloperEvaluation.WebApi.Features.CDBs.DeleteCDB;

/// <summary>
/// Validator for DeleteCDBRequest
/// </summary>
public class DeleteCDBRequestValidator : AbstractValidator<DeleteCDBRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCDBRequest
    /// </summary>
    public DeleteCDBRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CDB ID is required");
    }
}
