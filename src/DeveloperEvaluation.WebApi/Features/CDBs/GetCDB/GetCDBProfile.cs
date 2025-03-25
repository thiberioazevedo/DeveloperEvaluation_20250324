using AutoMapper;

namespace DeveloperEvaluation.WebApi.Features.Users.GetCDB;

public class GetCDBProfile : Profile
{
    public GetCDBProfile()
    {
        CreateMap<Guid, Application.Users.GetUser.GetUserCommand>().ConstructUsing(id => new Application.Users.GetUser.GetUserCommand(id));
        CreateMap<Application.CDBs.GetCDB.GetCDBResult, CDBs.GetCDB.GetCDBResponse>().AfterMap((i, o) => o.OrderMonth());
        CreateMap<Application.CDBs.GetCDB.MonthCDB, CDBs.GetCDB.MonthCDB>();
    }
}
