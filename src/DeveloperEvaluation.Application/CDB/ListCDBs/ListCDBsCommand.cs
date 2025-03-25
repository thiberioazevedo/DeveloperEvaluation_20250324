using DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace DeveloperEvaluation.Application.CDBs.ListCDBs;

/// <summary>
/// Command for retrieving a cDB by their ID
/// </summary>
public record ListCDBsCommand : IRequest<PaginatedList<CDBResult>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchText { get; set; }
    public string? ColumnOrder { get; set; }
    public bool Asc { get; set; }
}
