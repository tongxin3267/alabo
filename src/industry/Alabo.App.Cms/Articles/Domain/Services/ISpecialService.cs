using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public interface ISpecialService : IService<Special, ObjectId> {

        ServiceResult AddOrUpdate(Special model);

        /// <summary>
        ///     根据当前的标识，获取页面名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ismobile"></param>
        string GetPagePath(string key, bool ismobile);
    }
}