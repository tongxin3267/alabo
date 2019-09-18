using MongoDB.Bson;
using Alabo.App.Open.Kpi.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.Kpi.Domain.Repositories {

    public interface IGradeKpiRepository : IRepository<GradeKpi, ObjectId> {
    }
}