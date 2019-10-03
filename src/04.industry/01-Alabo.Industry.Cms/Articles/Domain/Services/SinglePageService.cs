using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public class SinglePageService : ServiceBase<SinglePage, ObjectId>, ISinglePageService
    {
        public SinglePageService(IUnitOfWork unitOfWork, IRepository<SinglePage, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        public SinglePage GetSingleView(object id)
        {
            var find = GetSingle(id);
            if (find == null) {
                return new SinglePage();
            }

            return find;
        }
    }
}