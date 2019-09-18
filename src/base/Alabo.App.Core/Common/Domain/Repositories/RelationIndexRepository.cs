using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    internal class RelationIndexRepository : RepositoryEfCore<RelationIndex, long>, IRelationIndexRepository {

        public RelationIndexRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}