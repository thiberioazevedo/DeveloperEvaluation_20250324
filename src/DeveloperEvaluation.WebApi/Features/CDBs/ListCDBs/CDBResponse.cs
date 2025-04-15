namespace DeveloperEvaluation.WebApi.Features.CDBs.ListCDBs;

public class CDBResponse
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public int Months { get; set; }
    public decimal CDI { get; set; }
    public decimal TB { get; set; }
    public ICollection<MonthCDBResponse>? MonthCDBCollection { get; set; }
}
