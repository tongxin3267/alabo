using Alabo.Domains.Services;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public interface IAboutService : IService<About, ObjectId>
    {
        /// <summary>
        ///     插入默认数据
        /// </summary>
        void InitialData();
    }
}