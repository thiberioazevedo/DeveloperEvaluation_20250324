using DeveloperEvaluation.Domain.Common;

namespace DeveloperEvaluation.Domain.Entities;

public class MonthCDB: BaseEntity
{
    public MonthCDB()
    {
    }

    public MonthCDB(int month, decimal initialValue, CDB cDB)
    {
        Month = month;
        InitialValue = initialValue;
        CDB = cDB;
    }

    public int Month { get; private set; }
    public decimal InitialValue { get; private set; }
    public decimal FinalValue { get; private set; } = 0;
    public Guid CDBId { get; private set; } = Guid.NewGuid();
    public virtual CDB? CDB { get; private set; }

    public void Calculate() {
        if (CDB == null)
            return;

        InitialValue = CDB.GrossValue;

        FinalValue = InitialValue * (1 + ((CDB.CDI + CDB.TB) / 100));

        FinalValue = Math.Round(FinalValue, 2);

        CDB.GrossValue = FinalValue;
    }
}