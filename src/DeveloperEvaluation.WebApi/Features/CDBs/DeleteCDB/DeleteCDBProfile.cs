using AutoMapper;

namespace DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteCDBProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCDB feature
    /// </summary>
    public DeleteCDBProfile()
    {
        CreateMap<Guid, Application.CDBs.DeleteCDB.DeleteCDBCommand>()
            .ConstructUsing(id => new Application.CDBs.DeleteCDB.DeleteCDBCommand(id));
    }
}
