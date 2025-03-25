using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperEvaluation.ORM.Repositories;

public class CDBRepository : Repository<CDB>, ICDBRepository
{
    public CDBRepository(DefaultContext defaultContext) : base(defaultContext)
    {
    }

    public override IQueryable<CDB> GetQuery()
    {
        return base.GetQuery().Include(i => i.MonthCDBCollection).OrderByDescending(i => i.Number);
    }
}
