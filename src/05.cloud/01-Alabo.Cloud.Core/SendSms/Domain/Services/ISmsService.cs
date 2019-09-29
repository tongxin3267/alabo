using _01_Alabo.Cloud.Core.SendSms.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Services
{
    /// <summary>
    ///     发送短信服务
    /// </summary>
    public interface ISmsService : IService<SmsSend, ObjectId>
    {
    }
}