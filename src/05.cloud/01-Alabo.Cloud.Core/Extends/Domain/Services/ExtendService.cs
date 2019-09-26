using _01_Alabo.Cloud.Core.Extends.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.Extends.Domain.Services
{
    public class ExtendService : ServiceBase<Extend, ObjectId>, IExtendService
    {
        public ExtendService(IUnitOfWork unitOfWork, IRepository<Extend, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}