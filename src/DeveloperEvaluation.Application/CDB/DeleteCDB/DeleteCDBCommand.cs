using MediatR;

namespace DeveloperEvaluation.Application.CDBs.DeleteCDB;

/// <summary>
/// Command for deleting a CDB in the system.
/// </summary>
public record DeleteCDBCommand : IRequest<DeleteCDBResponse>
{
    /// <summary>
    /// The unique identifier of the CDB to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteCDBCommand
    /// </summary>
    /// <param name="id">The ID of the CDB to delete</param>
    public DeleteCDBCommand(Guid id)
    {
        Id = id;
    }
}
