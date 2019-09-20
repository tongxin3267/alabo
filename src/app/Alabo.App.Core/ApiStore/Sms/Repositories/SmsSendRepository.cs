using MongoDB.Bson;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.ApiStore.Sms.Repositories {

    public class SmsSendRepository : RepositoryMongo<SmsSend, ObjectId>, ISmsSendRepository {

        public SmsSendRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}