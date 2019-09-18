using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public interface IAboutService : IService<About, ObjectId> {

        /// <summary>
        ///     插入默认数据
        /// </summary>
        void InitialData();
    }
}