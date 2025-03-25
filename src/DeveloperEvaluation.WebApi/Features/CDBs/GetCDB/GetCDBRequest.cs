namespace DeveloperEvaluation.WebApi.Features.CDBs.GetCDB;

/// <summary>
/// Request model for getting a cDB by ID
/// </summary>
public class GetCDBRequest
{
    /// <summary>
    /// The unique identifier of the cDB to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
