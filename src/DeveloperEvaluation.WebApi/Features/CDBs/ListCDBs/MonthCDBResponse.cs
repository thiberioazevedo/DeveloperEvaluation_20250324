namespace DeveloperEvaluation.WebApi.Features.CDBs.ListCDBs;

public class MonthCDBResponse 
{
    public int Month { get; set; }
    public decimal InitialValue { get; set; }
    public decimal TaxPercentage { get; set; }
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }
    public decimal TaxAmount { get; set; }

}
