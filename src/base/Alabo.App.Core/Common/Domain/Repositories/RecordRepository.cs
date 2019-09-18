using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public class RecordRepository : RepositoryEfCore<Record, long>, IRecordRepository {

        public RecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}