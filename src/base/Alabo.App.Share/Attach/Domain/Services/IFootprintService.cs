using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Dtos;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Open.Attach.Domain.Services {

    public interface IFootprintService : IService<Footprint, ObjectId> {

        /// <summary>
        /// ��Ʒ�㼣
        /// </summary>
        /// <param name="query"></param>
        PagedList<Footprint> GetProductPagedList(object query);

        /// <summary>
        /// ����㼣
        /// </summary>
        /// <param name="footprintInput"></param>
        /// <returns></returns>
        ServiceResult Add(FootprintInput footprintInput);

        /// <summary>
        /// ����㼣
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        ServiceResult Clear(long loginUserId);
    }
}