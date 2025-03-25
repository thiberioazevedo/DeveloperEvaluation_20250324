using DeveloperEvaluation.Domain.Common;
using DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace DeveloperEvaluation.Domain.Entities;

public class CDB : BaseEntity
{
    public CDB()
    {
    }

    public CDB(decimal value, int months, decimal cDI, decimal tB)
    {
        Value = value;
        Months = months;
        CDI = cDI;
        TB = tB;
    }

    public decimal Value { get; internal set; }
    public int Months { get; internal set; }
    public decimal CDI { get; internal set; }
    public decimal TB { get; internal set; }
    public decimal GrossValue { get; internal set; }
    public decimal TaxPercentage { get; internal set; }
    public decimal TaxAmount { get; internal set; }
    public decimal NetValue { get; internal set; }
    public virtual ICollection<MonthCDB>? MonthCDBCollection { get; internal set; }
    public override ValidationResult GetValidationResult()
    {
        return new CDBValidator().Validate(this);
    }
    public void Calculate(decimal cDI, decimal tB) {
        CDI = cDI;
        TB = tB;

        MonthCDBCollection = new List<MonthCDB>();

        GrossValue = Value;

        for (var i = 0; i < Months; i++)
        {
            var monthCDB = new MonthCDB(i + 1, GrossValue, this);

            monthCDB.Calculate();

            MonthCDBCollection.Add(monthCDB);
        }

        SetTaxPercentage();
        SetTaxAmount();
        SetNetValue();
    }

    void SetTaxPercentage()
    {
        TaxPercentage = Months switch
        {
            int n when (n <= 6) => 0.225m,
            int n when (n <= 12) => 0.20m,
            int n when (n <= 24) => 0.175m,
            _ => 0.15m,
        };
    }

    void SetTaxAmount() {
        TaxAmount = Math.Round(GrossValue * TaxPercentage, 2);
    }

    void SetNetValue() {
        NetValue = GrossValue - TaxAmount;
    }
}