using _01_Alabo.Cloud.Core.SendSms.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Services
{
    /// <summary>
    ///     短信服务
    /// </summary>
    public class SmsService : ServiceBase<SmsSend, ObjectId>, ISmsService
    {
        public SmsService(IUnitOfWork unitOfWork, IRepository<SmsSend, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}