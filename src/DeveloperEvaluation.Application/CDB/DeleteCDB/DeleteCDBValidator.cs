using FluentValidation;

namespace DeveloperEvaluation.Application.CDBs.DeleteCDB;

/// <summary>
/// Validator for DeleteCDBCommand
/// </summary>
public class DeleteCDBValidator : AbstractValidator<DeleteCDBCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCDBCommand
    /// </summary>
    public DeleteCDBValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CDB ID is required");
    }
}
