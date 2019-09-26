using Alabo.Domains.Services;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services {

    public interface ISinglePageService : IService<SinglePage, ObjectId> {

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SinglePage GetSingleView(object id);
    }
}