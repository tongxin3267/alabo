using Alabo.Cloud.Shop.SuccessfulCases.Domains.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.SuccessfulCases.Domains.Services {

    public class CasesService : ServiceBase<Cases, ObjectId>, ICasesService {

        public CasesService(IUnitOfWork unitOfWork, IRepository<Cases, ObjectId> repository) : base(unitOfWork,
            repository) {
        }


        public Entities.Cases GetCourseView(object id)
        {
            var find = GetSingle(id);
            if (find == null)
            {
                return new Entities.Cases(); ;
            }

            return find;
        }

        public ServiceResult AddOrUpdate(Entities.Cases view)
        {
            var find = GetSingle(view.Id);
            if (find == null)
            {
                if (Add(view))
                {
                    return ServiceResult.Success;
                }
            }
            else
            {
                if (Update(view))
                {
                    return ServiceResult.Success;
                }
            }
            return ServiceResult.Failed;
        }
    }
}