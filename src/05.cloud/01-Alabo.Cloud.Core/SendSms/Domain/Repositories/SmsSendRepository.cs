using _01_Alabo.Cloud.Core.SendSms.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Repositories
{
    public class SmsSendRepository : RepositoryMongo<SmsSend, ObjectId>, ISmsSendRepository
    {
        public SmsSendRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}