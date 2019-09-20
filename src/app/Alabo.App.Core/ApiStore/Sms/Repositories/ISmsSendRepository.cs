using MongoDB.Bson;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.ApiStore.Sms.Repositories {

    public interface ISmsSendRepository : IRepository<SmsSend, ObjectId> {
    }
}