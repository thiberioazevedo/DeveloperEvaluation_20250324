
namespace DeveloperEvaluation.Application.CDBs.GetCDB;

public class GetCDBResult
{
    public decimal Value { get; set; }
    public int Months { get; set; }
    public decimal CDI { get; set; }
    public decimal TB { get; set; }
    public decimal GrossValue { get; set; }
    public decimal TaxPercentage { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetValue { get; set; }
    public virtual ICollection<MonthCDB>? MonthCDBCollection { get; set; }
}
