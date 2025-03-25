using AutoMapper;

namespace DeveloperEvaluation.Application.CDBs.CreateCDB;

/// <summary>
/// Profile for mapping between CDB entity and CreateCDBResponse
/// </summary>
public class CreateCDBProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCDB operation
    /// </summary>
    public CreateCDBProfile()
    {
        CreateMap<CreateCDBCommand, Domain.Entities.CDB>();
        CreateMap<Domain.Entities.CDB, CreateCDBResult>();
        CreateMap<Domain.Entities.MonthCDB, MonthCDB>();
    }
}
