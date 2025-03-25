using AutoMapper;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;

namespace DeveloperEvaluation.Application.CDBs.ListCDBs;

/// <summary>
/// Profile for mapping between CDB entity and GetCDBResponse
/// </summary>
public class ListCDBsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCDB operation
    /// </summary>
    public ListCDBsProfile()
    {
        CreateMap<CDB, CDBResult>();
        CreateMap<PaginatedList<CDB>, PaginatedList<CDBResult>>();
    }
}
