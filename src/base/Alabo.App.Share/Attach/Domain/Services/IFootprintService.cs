using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Dtos;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Open.Attach.Domain.Services {

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