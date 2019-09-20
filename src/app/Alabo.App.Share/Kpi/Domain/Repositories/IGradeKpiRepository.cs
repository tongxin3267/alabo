using MongoDB.Bson;
using Alabo.App.Share.Kpi.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Kpi.Domain.Repositories {

    public interface IGradeKpiRepository : IRepository<GradeKpi, ObjectId> {
    }
}