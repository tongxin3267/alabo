using System.Collections.Generic;
using Alabo.Data.People.Circles.Client;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Mapping;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Domain.Services {

    public class CircleService : ServiceBase<Entities.Circle, ObjectId>, ICircleService {

        public CircleService(IUnitOfWork unitOfWork, IRepository<Entities.Circle, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public void Init() {
            if (!Exists()) {
                var client = new CircleMapClient();
                var circleList = client.GetCircleList();
                var result = new List<Entities.Circle>();
                foreach (var item in circleList) {
                    var view = AutoMapping.SetValue<Entities.Circle>(item);
                    result.Add(view);
                }

                AddMany(result);
            }
        }
    }
}