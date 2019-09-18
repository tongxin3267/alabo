using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public interface ISinglePageService : IService<SinglePage, ObjectId> {

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SinglePage GetSingleView(object id);
    }
}