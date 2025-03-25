using MediatR;
using FluentValidation;
using DeveloperEvaluation.Domain.Repositories;

namespace DeveloperEvaluation.Application.CDBs.DeleteCDB;

/// <summary>
/// Handler for processing DeletCDBCommand requests
/// </summary>
public class DeleteCDBHandler : IRequestHandler<DeleteCDBCommand, DeleteCDBResponse>
{
    private readonly ICDBRepository _cDBRepository;

    /// <summary>
    /// Initializes a new instance of DeleteCDBHandler
    /// </summary>
    /// <param name="cDBRepository">The cDB repository</param>
    /// <param name="validator">The validator for DeleteCDBCommand</param>
    public DeleteCDBHandler(
        ICDBRepository cDBRepository)
    {
        _cDBRepository = cDBRepository;
    }

    /// <summary>
    /// Handles the DeleteCDBCommand request
    /// </summary>
    /// <param name="request">The DeleteCDB command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCDBResponse> Handle(DeleteCDBCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCDBValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _cDBRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"CDB with ID {request.Id} not found");

        return new DeleteCDBResponse { Success = true };
    }
}
