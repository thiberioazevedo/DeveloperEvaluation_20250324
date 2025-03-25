using AutoMapper;

namespace DeveloperEvaluation.Application.CDBs.GetCDB;

public class GetCDBProfile : Profile
{
    public GetCDBProfile()
    {
        CreateMap<GetCDBCommand, Domain.Entities.CDB > ();
        CreateMap<Domain.Entities.CDB, GetCDBResult>();
        CreateMap<Domain.Entities.MonthCDB, MonthCDB>();
    }
}
