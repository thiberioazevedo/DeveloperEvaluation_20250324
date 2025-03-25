using AutoMapper;
using DeveloperEvaluation.Application.CDBs.CreateCDB;

namespace DeveloperEvaluation.WebApi.Features.CDBs.CreateCDB;

public class CreateCDBProfile : Profile
{
    public CreateCDBProfile()
    {
        CreateMap<CreateCDBRequest, CreateCDBCommand>();
        CreateMap<CreateCDBResult, CreateCDBResponse>();
        CreateMap<Application.CDBs.CreateCDB.MonthCDB, MonthCDB>();
    }
}
