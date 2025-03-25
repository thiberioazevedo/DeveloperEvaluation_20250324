namespace DeveloperEvaluation.WebApi.Features.CDBs.DeleteCDB;

/// <summary>
/// Request model for deleting a cDB
/// </summary>
public class DeleteCDBRequest
{
    /// <summary>
    /// The unique identifier of the cDB to delete
    /// </summary>
    public Guid Id { get; set; }
}
