using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.ORM.Repositories;

namespace DeveloperEvaluation.Unit.Domain.Repositories
{
    public class CDBRepositoryTests: BaseTest<CDB>
    {
        public override CDB CreateEntityDefaultInstance()
        {
            return new CDB(1, 2);
        }

        public override IRepository<CDB> CreateRepositoryInstance()
        {
            return new CDBRepository(BaseTestHelpers.GetCreateDefaultContextInstance());
        }
    }
}