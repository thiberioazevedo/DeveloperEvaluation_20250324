namespace DeveloperEvaluation.Application.CDBs.CreateCDB;

public class CreateCDBResult
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public int Months { get; set; }
    public decimal CDI { get; set; }
    public decimal TB { get; set; }
    public ICollection<MonthCDB> MonthCDBCollection { get; set; } = [];
}
