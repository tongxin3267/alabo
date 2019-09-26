using Alabo.App.Kpis.GradeKpis.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Kpis.GradeKpis.Domain.Repositories {

    public class GradeKpiRepository : RepositoryMongo<GradeKpi, ObjectId>, IGradeKpiRepository {

        public GradeKpiRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}