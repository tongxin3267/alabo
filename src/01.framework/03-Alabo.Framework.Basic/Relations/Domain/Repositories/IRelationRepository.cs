using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public interface IRelationRepository : IRepository<Relation, long> {
    }
}