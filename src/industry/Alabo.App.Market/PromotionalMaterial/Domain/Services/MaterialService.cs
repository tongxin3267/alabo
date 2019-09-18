using MongoDB.Bson;
using Alabo.App.Market.PromotionalMaterial.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Market.PromotionalMaterial.Domain.Services {

    public class MaterialService : ServiceBase<Material, ObjectId>, IMaterialService {

        public MaterialService(IUnitOfWork unitOfWork, IRepository<Material, ObjectId> repository) : base(unitOfWork,
            repository) {

        }

            public Material GetCourseView(object id)
            {
                var find = GetSingle(id);
                if (find == null)
                {
                    return new Material(); ;
                }

                return find;
            }

            public ServiceResult AddOrUpdate(Material view)
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