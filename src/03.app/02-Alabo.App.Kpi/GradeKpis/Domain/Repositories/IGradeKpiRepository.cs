using Alabo.App.Kpis.GradeKpis.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Kpis.GradeKpis.Domain.Repositories
{
    public interface IGradeKpiRepository : IRepository<GradeKpi, ObjectId>
    {
    }
}