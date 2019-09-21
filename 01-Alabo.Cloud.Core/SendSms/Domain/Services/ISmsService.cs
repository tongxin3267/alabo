using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace ZKCloud.App.Core.ApiStore.Sms.Services {

    /// <summary>
    /// 发送短信服务
    /// </summary>
    public interface ISmsService : IService<SmsSend, ObjectId> {
    }
}