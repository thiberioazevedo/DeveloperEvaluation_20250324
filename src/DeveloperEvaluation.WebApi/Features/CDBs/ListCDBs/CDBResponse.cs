using DeveloperEvaluation.Domain.Enums;

namespace DeveloperEvaluation.WebApi.Features.CDBs.ListCDBs;

public class CDBResponse
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public int Months { get; set; }
    public decimal CDI { get; set; }
    public decimal TB { get; set; }
    public decimal GrossValue { get; set; }
    public decimal TaxPercentage { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetValue { get; set; }
}
