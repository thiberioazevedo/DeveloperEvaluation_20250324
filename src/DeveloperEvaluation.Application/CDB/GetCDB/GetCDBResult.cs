
namespace DeveloperEvaluation.Application.CDBs.GetCDB;

public class GetCDBResult
{
    public decimal Value { get; set; }
    public int Months { get; set; }
    public decimal CDI { get; set; }
    public decimal TB { get; set; }
    public virtual ICollection<MonthCDB>? MonthCDBCollection { get; set; }
}
