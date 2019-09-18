using MongoDB.Bson;
using Alabo.App.Open.Kpi.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.Kpi.Domain.Repositories {

    public class GradeKpiRepository : RepositoryMongo<GradeKpi, ObjectId>, IGradeKpiRepository {

        public GradeKpiRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}