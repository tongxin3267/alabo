using MongoDB.Bson;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface ICircleService : IService<Circle, ObjectId> {

        /// <summary>
        ///     初始商圈数据
        /// </summary>
        void Init();
    }
}