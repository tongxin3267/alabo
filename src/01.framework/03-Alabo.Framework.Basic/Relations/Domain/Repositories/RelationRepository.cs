using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Relations.Domain.Entities;

namespace Alabo.Framework.Basic.Relations.Domain.Repositories
{
    internal class RelationRepository : RepositoryEfCore<Relation, long>, IRelationRepository
    {
        public RelationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}