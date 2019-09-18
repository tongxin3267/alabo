using MongoDB.Bson;
using Alabo.App.Share.Kpi.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Kpi.Domain.Repositories {

    public class GradeKpiRepository : RepositoryMongo<GradeKpi, ObjectId>, IGradeKpiRepository {

        public GradeKpiRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}