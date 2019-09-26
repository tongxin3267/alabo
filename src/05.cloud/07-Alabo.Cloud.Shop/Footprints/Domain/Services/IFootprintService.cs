using Alabo.Cloud.Shop.Footprints.Domain.Entities;
using Alabo.Cloud.Shop.Footprints.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Footprints.Domain.Services {

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