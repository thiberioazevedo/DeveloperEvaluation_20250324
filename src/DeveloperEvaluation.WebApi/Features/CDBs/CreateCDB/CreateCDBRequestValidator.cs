using DeveloperEvaluation.Domain.Enums;
using DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace DeveloperEvaluation.WebApi.Features.CDBs.CreateCDB;

/// <summary>
/// Validator for CreateCDBRequest that defines validation rules for cDB creation.
/// </summary>
public class CreateCDBRequestValidator : AbstractValidator<CreateCDBRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCDBRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public CreateCDBRequestValidator()
    {
    }
}