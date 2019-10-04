using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Framework.Core.Admins.Repositories;
using System.Collections.Generic;

namespace Alabo.Framework.Core.Admins.Services {

    /// <summary>
    ///     Class CatalogService.
    /// </summary>
    public class CatalogService : ServiceBase, ICatalogService {

        /// <summary>
        ///     The catalog repository
        /// </summary>
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(IUnitOfWork unitOfWork) : base(unitOfWork) {
            _catalogRepository = Repository<ICatalogRepository>();
        }

        /// <summary>
        ///     数据库维护脚本
        /// </summary>
        public void UpdateDatabase() {
            _catalogRepository.UpdateDataBase();
        }

        /// <summary>
        ///     获取所有的Sql表实体
        /// </summary>
        public List<string> GetSqlTable() {
            return _catalogRepository.GetSqlTable();
        }
    }
}