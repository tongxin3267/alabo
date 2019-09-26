using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Relations.Domain.Entities;

namespace Alabo.Framework.Basic.Relations.Domain.Repositories
{
    internal class RelationIndexRepository : RepositoryEfCore<RelationIndex, long>, IRelationIndexRepository
    {
        public RelationIndexRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}