using Microsoft.EntityFrameworkCore;

namespace Alabo.Datas.UnitOfWorks.PgSql {

    /// <summary>
    ///     工作单元
    /// </summary>
    public class PgSqlUnitOfWork : Ef.PgSql.PgSqlUnitOfWork, IUnitOfWork {

        /// <summary>
        ///     初始化工作单元
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="unitOfWorkManager">工作单元服务</param>
        public PgSqlUnitOfWork(DbContextOptions options, IUnitOfWorkManager unitOfWorkManager) : base(options,
            unitOfWorkManager) {
        }
    }
}