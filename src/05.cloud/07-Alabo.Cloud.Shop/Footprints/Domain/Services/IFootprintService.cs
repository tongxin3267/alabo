using Alabo.Cloud.Shop.Footprints.Domain.Entities;
using Alabo.Cloud.Shop.Footprints.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Footprints.Domain.Services {

    public interface IFootprintService : IService<Footprint, ObjectId> {

        /// <summary>
        /// ÉÌÆ·×ã¼£
        /// </summary>
        /// <param name="query"></param>
        PagedList<Footprint> GetProductPagedList(object query);

        /// <summary>
        /// Ìí¼Ó×ã¼£
        /// </summary>
        /// <param name="footprintInput"></param>
        /// <returns></returns>
        ServiceResult Add(FootprintInput footprintInput);

        /// <summary>
        /// Çå¿Õ×ã¼£
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        ServiceResult Clear(long loginUserId);
    }
}