using AutoMapper;
using DeveloperEvaluation.Application.CDBs.ListCDBs;
using DeveloperEvaluation.Domain.Repositories;

namespace DeveloperEvaluation.WebApi.Features.CDBs.ListCDBs;

/// <summary>
/// Profile for mapping GetCDB feature requests to commands
/// </summary>
public class ListCDBsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCDB feature
    /// </summary>
    public ListCDBsProfile()
    {
        CreateMap<ListCDBsRequest, ListCDBsCommand> ();
        CreateMap<PaginatedList<CDBResult>, PaginatedList<CDBResponse>>();
        CreateMap<CDBResult, CDBResponse>();
        CreateMap<MonthCDBResult, MonthCDBResponse>();
    }
}
