using MongoDB.Bson;
using Alabo.App.Core.ApiStore.AMap.CircleMap;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Common.Domain.Services {

    public class CircleService : ServiceBase<Circle, ObjectId>, ICircleService {

        public CircleService(IUnitOfWork unitOfWork, IRepository<Circle, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        public void Init() {
            if (!Exists()) {
                var client = new CircleMapClient();
                var circleList = client.GetCircleList();
                //circleList.Foreach(r => {
                //    r.Id = ObjectId.Empty;
                //});
                //  AddMany(circleList);
            }
        }
    }
}