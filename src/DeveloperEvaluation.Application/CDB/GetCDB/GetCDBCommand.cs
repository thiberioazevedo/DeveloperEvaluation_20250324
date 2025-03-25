using MediatR;

namespace DeveloperEvaluation.Application.CDBs.GetCDB;

/// <summary>
/// Command for retrieving a cDB by their ID
/// </summary>
public record GetCDBCommand : IRequest<GetCDBResult>
{
    /// <summary>
    /// The unique identifier of the cDB to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetCDBCommand
    /// </summary>
    /// <param name="id">The ID of the cDB to retrieve</param>
    public GetCDBCommand(Guid id)
    {
        Id = id;
    }
}
