using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Common.Domain.Services {

    public class RecordService : ServiceBase<Record, long>, IRecordService {

        public RecordService(IUnitOfWork unitOfWork, IRepository<Record, long> repository) : base(unitOfWork,
            repository) {
        }
    }
}