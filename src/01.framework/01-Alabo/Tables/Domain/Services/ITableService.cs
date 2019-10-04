using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Tables.Domain.Entities;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.Tables.Domain.Services {

    /// <summary>
    ///     表结构相关服务
    /// </summary>
    public interface ITableService : IService<Table, ObjectId> {

        /// <summary>
        ///     初始化所有的表
        /// </summary>
        void Init();

        List<KeyValue> MongodbCatalogEntityKeyValues();

        List<KeyValue> SqlServcieCatalogEntityKeyValues();

        List<KeyValue> CatalogEntityKeyValues();
    }
}