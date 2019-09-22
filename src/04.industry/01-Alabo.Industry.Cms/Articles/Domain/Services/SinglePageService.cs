using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public class SinglePageService : ServiceBase<SinglePage, ObjectId>, ISinglePageService {

        public SinglePageService(IUnitOfWork unitOfWork, IRepository<SinglePage, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public SinglePage GetSingleView(object id) {
            var find = GetSingle(id);
            if (find == null) {
                return new SinglePage();
            }

            return find;
        }
    }
}