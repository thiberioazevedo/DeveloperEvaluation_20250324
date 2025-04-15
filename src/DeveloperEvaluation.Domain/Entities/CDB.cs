using DeveloperEvaluation.Domain.Common;
using DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace DeveloperEvaluation.Domain.Entities;

public class CDB : BaseEntity
{
    public CDB()
    {
    }

    public CDB(decimal value, int months)
    {
        Value = value;
        Months = months;

        MonthCDBCollection = new List<MonthCDB>();
    }

    public decimal Value { get; internal set; }
    public int Months { get; internal set; }
    public decimal CDI { get; internal set; }
    public decimal TB { get; internal set; }
    public virtual ICollection<MonthCDB>? MonthCDBCollection { get; internal set; }
    public override ValidationResult GetValidationResult()
    {
        return new CDBValidator().Validate(this);
    }
    public void GenerateMonths(decimal cDI, decimal tB) {
        CDI = cDI;
        TB = tB;

        for (var i = 0; i < Months; i++)
        {
            var monthCDB = new MonthCDB(i + 1, this);

            monthCDB.Calculate();

            MonthCDBCollection.Add(monthCDB);
        }
    }
}