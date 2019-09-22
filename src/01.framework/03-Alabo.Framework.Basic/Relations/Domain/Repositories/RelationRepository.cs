using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    internal class RelationRepository : RepositoryEfCore<Relation, long>, IRelationRepository {

        public RelationRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}