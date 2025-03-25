using AutoMapper;
using MediatR;
using FluentValidation;
using DeveloperEvaluation.Domain.Repositories;

namespace DeveloperEvaluation.Application.CDBs.GetCDB;

/// <summary>
/// Handler for processing GetCDBCommand requests
/// </summary>
public class GetCDBHandler : IRequestHandler<GetCDBCommand, GetCDBResult>
{
    private readonly ICDBRepository _cDBRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCDBHandler
    /// </summary>
    /// <param name="cDBRepository">The cDB repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCDBCommand</param>
    public GetCDBHandler(
        ICDBRepository cDBRepository,
        IMapper mapper)
    {
        _cDBRepository = cDBRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCDBCommand request
    /// </summary>
    /// <param name="request">The GetCDB command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cDB details if found</returns>
    public async Task<GetCDBResult> Handle(GetCDBCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCDBValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cDB = await _cDBRepository.GetByIdAsync(request.Id, cancellationToken);

        return cDB == null ? throw new KeyNotFoundException($"CDB with ID {request.Id} not found") : _mapper.Map<GetCDBResult>(cDB);
    }
}
