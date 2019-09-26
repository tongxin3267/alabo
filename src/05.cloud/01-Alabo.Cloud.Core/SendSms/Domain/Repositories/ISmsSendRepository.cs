using _01_Alabo.Cloud.Core.SendSms.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Repositories {

    public interface ISmsSendRepository : IRepository<SmsSend, ObjectId> {
    }
}