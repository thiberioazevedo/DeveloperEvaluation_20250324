using AutoMapper;
using DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace DeveloperEvaluation.Application.CDBs.ListCDBs;

/// <summary>
/// Handler for processing GetCDBCommand requests
/// </summary>
public class ListCDBsHandler : IRequestHandler<ListCDBsCommand, PaginatedList<CDBResult>>
{
    private readonly ICDBRepository _cDBRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCDBHandler
    /// </summary>
    /// <param name="cDBRepository">The cDB repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCDBCommand</param>
    public ListCDBsHandler(
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
    public async Task<PaginatedList<CDBResult>> Handle(ListCDBsCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListCDBsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList = await _cDBRepository.GetPaginatedList(request.SearchText, request.ColumnOrder, request.Asc, request.PageNumber, request.PageSize, cancellationToken);

        return _mapper.Map<PaginatedList<CDBResult>>(paginatedList);
    }
}
