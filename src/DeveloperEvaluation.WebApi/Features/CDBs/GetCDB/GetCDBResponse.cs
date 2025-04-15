using DeveloperEvaluation.Domain.Enums;

namespace DeveloperEvaluation.WebApi.Features.CDBs.GetCDB;

/// <summary>
/// API response model for GetCDB operation
/// </summary>
public class GetCDBResponse
{
    public decimal Value { get; internal set; }
    public int Months { get; internal set; }
    public decimal CDI { get; internal set; }
    public decimal TB { get; internal set; }
    public virtual ICollection<MonthCDB>? MonthCDBCollection { get; internal set; }
    internal void OrderMonth()
    {
        if (MonthCDBCollection != null)
            MonthCDBCollection = MonthCDBCollection.OrderBy(i => i.Month).ToList();
    }
}
