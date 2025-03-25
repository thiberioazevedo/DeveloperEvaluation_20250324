using FluentValidation;

namespace DeveloperEvaluation.Application.CDBs.GetCDB;

/// <summary>
/// Validator for GetCDBCommand
/// </summary>
public class GetCDBValidator : AbstractValidator<GetCDBCommand>
{
    /// <summary>
    /// Initializes validation rules for GetCDBCommand
    /// </summary>
    public GetCDBValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CDB ID is required");
    }
}
