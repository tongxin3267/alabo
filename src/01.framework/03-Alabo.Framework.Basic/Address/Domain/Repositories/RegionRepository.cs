using MongoDB.Bson;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public class RegionRepository : RepositoryMongo<Region, ObjectId>, IRegionRepository {

        public RegionRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}