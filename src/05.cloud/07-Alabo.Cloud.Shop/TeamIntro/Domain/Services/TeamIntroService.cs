using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.TeamIntro.Domain.Services
{
    public class TeamIntroService : ServiceBase<Entities.TeamIntro, ObjectId>, ITeamIntroService
    {
        public TeamIntroService(IUnitOfWork unitOfWork, IRepository<Entities.TeamIntro, ObjectId> repository) : base(
            unitOfWork,
            repository)
        {
        }

        public Entities.TeamIntro GetCourseView(object id)
        {
            var find = GetSingle(id);
            if (find == null)
            {
                return new Entities.TeamIntro();
                ;
            }

            return find;
        }

        public ServiceResult AddOrUpdate(Entities.TeamIntro view)
        {
            var find = GetSingle(view.Id);
            if (find == null)
            {
                if (Add(view)) return ServiceResult.Success;
            }
            else
            {
                if (Update(view)) return ServiceResult.Success;
            }

            return ServiceResult.Failed;
        }
    }
}