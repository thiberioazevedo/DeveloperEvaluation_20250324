using DeveloperEvaluation.Domain.Common;

namespace DeveloperEvaluation.Domain.Entities;

public class MonthCDB: BaseEntity
{
    public MonthCDB()
    {
    }

    public MonthCDB(int month, CDB cDB)
    {
        Month = month;
        CDB = cDB;
        InitialValue = (CDB.MonthCDBCollection?.Where(i => i.Month < Month).OrderBy(i => i.Month).LastOrDefault()?.GrossValue) ?? CDB.Value;
    }

    public int Month { get; private set; }
    public decimal InitialValue { get; private set; }
    public decimal TaxPercentage { get; internal set; }
    public decimal GrossValue { get; internal set; }
    public decimal NetValue { get; internal set; }
    public decimal TaxAmount { get; internal set; }
    public Guid CDBId { get; private set; } = Guid.NewGuid();
    public virtual CDB? CDB { get; private set; }
    public void Calculate() {
        if (CDB == null)
            return;

        SetTaxPercentage();

        GrossValue = InitialValue * (1 + (CDB.CDI / 100 * CDB.TB / 100));

        SetTaxAmount();

        SetNetValue();
    }
    void SetTaxPercentage()
    {
        TaxPercentage = Month switch
        {
            int n when (n <= 6) => 0.225m,
            int n when (n <= 12) => 0.20m,
            int n when (n <= 24) => 0.175m,
            _ => 0.15m,
        };
    }
    void SetTaxAmount()
    {
        TaxAmount = (GrossValue - CDB.Value) * TaxPercentage;
    }
    void SetNetValue()
    {
        NetValue = GrossValue - TaxAmount;
    }
}